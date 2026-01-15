using System.Net.Http.Json;
using MiniCatalog.Application.DTOs.Audit;
using MiniCatalog.Application.DTOs.Import;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Services;

public class ImportService : IImportService
{
    private readonly IItemRepository _itemRepo;
    private readonly ICategoriaRepository _categoriaRepo;
    private readonly IAuditService _auditLogService;
    private readonly HttpClient _httpClient;
    
    private const string ExternalApiUrl = "https://fakestoreapi.com/products";

    public ImportService(
        IItemRepository itemRepo,
        ICategoriaRepository categoriaRepo,
        IAuditService auditService,
        HttpClient httpClient)
    {
        _itemRepo = itemRepo;
        _categoriaRepo = categoriaRepo;
        _auditLogService = auditService;
        _httpClient = httpClient;
    }

    public async Task<ImportResultDto> ImportFromExternalApiAsync(Guid userId)
    {
        var messages = new List<string>();
        int imported = 0;
        int skipped = 0;
        
        var response = await _httpClient.GetFromJsonAsync<List<ImportDto>>(ExternalApiUrl);
        
        if (response == null || !response.Any())
            return new ImportResultDto(0, 0, 0, new List<string> { "API externa não retornou dados." });

        foreach (var dto in response)
        {
            try
            {
                var categoryName = dto.Category ?? "Geral";
                var categoryDescription = dto.Description ?? "Sem descricao";
                var categoria = await _categoriaRepo.GetByNameAsync(categoryName);
    
                if (categoria == null)
                {
                    categoria = new CategoriaModel(categoryName, categoryDescription);
        
                    await _categoriaRepo.AddAsync(categoria);
                }

                if (await _itemRepo.ExistsAsync(dto.Title, categoria.Id))
                {
                    skipped++;
                    messages.Add($"Ignorado: '{dto.Title}' já existe.");
                    continue; 
                }
                
                var item = new ItemModel(
                    dto.Title,
                    dto.Description,
                    categoria.Id,
                    dto.Price
                );

                await _itemRepo.AddAsync(item);
                imported++;
            }
            catch (Exception ex)
            {
                messages.Add($"Erro em '{dto.Title}': {ex.Message}");
            }
        }
        
        await _auditLogService.AuditLogAsync(new AuditLogDto
        (
            Guid.NewGuid(),
            "IMPORT_EXECUTED",
            new { 
                TotalFound = response.Count, 
                ImportedCount = imported, 
                SkippedCount = skipped 
            },
            userId,
            DateTime.UtcNow
        ));

        return new ImportResultDto(response.Count, imported, skipped, messages);
    }
}