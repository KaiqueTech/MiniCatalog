using MiniCatalog.Application.DTOs.Audit;
using MiniCatalog.Application.DTOs.Categoria;
using MiniCatalog.Application.Exceptions;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Services;

public class CategoriaService
{
    private readonly ICategoriaRepository _categoryRepository;
    private readonly IAuditService _auditService;

    public CategoriaService(
        ICategoriaRepository categoriaRepository,
        IAuditService auditService)
    {
        _categoryRepository = categoriaRepository;
        _auditService = auditService;
    }

    public async Task<Guid> CreateAsync(CategoriaRequestDto dto, Guid userId)
    {
        if (await _categoryRepository.ExistsAsync(dto.Nome))
            throw new BusinessException("Já existe uma categoria com esse nome.");
        
        var category = new CategoriaModel(dto.Nome, dto.Descricao);
        
        await _categoryRepository.AddAsync(category);
        
        var auditLogDto = new AuditLogDto
        {
            Action = "CATEGORIA_CRIADA",
            UserId = userId,
            Payload = new { category.Id, category.Nome },
            Timestamp = DateTime.UtcNow
        };
        await _auditService.AuditLogAsync(auditLogDto);
        
        return category.Id;
    }

    public async Task<CategoriaResponseDto?> GetByIdAsync(Guid categoryId)
    {
        var categoria = await _categoryRepository.GetByIdAsync(categoryId);
        if (categoria == null) return null;

        return new CategoriaResponseDto
        (
            categoria.Id,
            categoria.Nome,
            categoria.Descricao,
            categoria.Ativa
        );
    }
    
    public async Task<IEnumerable<CategoriaResponseDto>> GetAllAsync()
    {
        var categorias = await _categoryRepository.GetAllAsync();
        
        return categorias.Select(c => new CategoriaResponseDto(
            c.Id,                                     
            c.Nome,
            c.Descricao,
            c.Ativa
        ));
    }

    public async Task ActivateAsync(Guid categoriaId, Guid userId)
    {
        var categoria = await _categoryRepository.GetByIdAsync(categoriaId)
                        ?? throw new NotFoundException("Categoria não encontrada.");
        
        categoria.Ativar();
        await _categoryRepository.UpdateAsync(categoria);
        
        var auditLogDto = new AuditLogDto
        {
            Action = "CATEGORIA_ATIVADA",
            UserId = userId,
            Payload = new { categoria.Id, categoria.Nome },
            Timestamp = DateTime.UtcNow
        };
        await _auditService.AuditLogAsync(auditLogDto);
    }

    public async Task DesactivateAsync(Guid categoryId, Guid userId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId)
                       ?? throw new NotFoundException("Categoria não encontrada.");

        category.Desativar();
        await _categoryRepository.UpdateAsync(category);
        
        var auditLogDto = new AuditLogDto
        {
            Action = "CATEGORIA_DESATIVADA",
            UserId = userId,
            Payload = new { category.Id, category.Nome },
            Timestamp = DateTime.UtcNow
        };
        await _auditService.AuditLogAsync(auditLogDto);
    }
}