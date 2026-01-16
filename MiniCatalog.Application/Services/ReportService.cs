using System.Text;
using MiniCatalog.Application.DTOs.Audit;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Application.Interfaces.Services;

namespace MiniCatalog.Application.Services;

public class ReportService : IReportService
{
    private readonly IItemRepository _itemRepository;
    private readonly IAuditService _auditLogService;

    public ReportService(IItemRepository itemRepository, IAuditService auditLogService)
    {
        _itemRepository = itemRepository;      
        _auditLogService = auditLogService;
    }
    public async Task<(byte[] Content, string FileName)> ExportItemsToCsvAsync(Guid userId)
    {
        var items = await _itemRepository.GetAllWithCategoryAsync();
        var ativos = items.Where(i => i.Ativo).ToList();

        var csv = new StringBuilder();
        csv.AppendLine("ID;Nome;Preco;Categoria;Status");

        foreach (var i in ativos)
        {
            csv.AppendLine($"{i.Id};{i.Nome};{i.Preco:F2};{i.Categoria?.Nome ?? "Sem Categoria"};Ativo");
        }
        
        csv.AppendLine();
        csv.AppendLine("--- ESTATISTICAS ---");
        csv.AppendLine($"Quantidade total de ativos:;{ativos.Count}");
        csv.AppendLine($"Qtd por Categoria:;{ativos.GroupBy(i => i.CategoriaId).Count()}");
        csv.AppendLine($"Preco Medio:;{(ativos.Any() ? ativos.Average(x => x.Preco) : 0):F2}");
        
        csv.AppendLine("Top 3 Mais Caros:");
        var top3 = ativos.OrderByDescending(i => i.Preco).Take(3);
        foreach(var t in top3) csv.AppendLine($"- {t.Nome};{t.Preco:F2}");
        
        var fileName = $"relatorio_itens_{DateTime.Now:yyyyMMdd_HHmm}.csv";
        
        await _auditLogService.AuditLogAsync(new AuditLogDto
        (
            Guid.NewGuid(),
            "EXPORT_EXECUTED",
            new { 
                FileName = fileName,
                TotalItems = ativos.Count,
                Format = "CSV"
            },
            userId,
            DateTime.UtcNow
        ));

        return (Encoding.UTF8.GetBytes(csv.ToString()), $"relatorio_{DateTime.Now:yyyyMMdd_HHmm}.csv");
    }
}