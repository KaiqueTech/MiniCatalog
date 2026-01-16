using FluentValidation;
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
    private IValidator<CategoriaRequestDto> _validator;

    public CategoriaService(ICategoriaRepository categoriaRepository, IAuditService auditService, IValidator<CategoriaRequestDto> validator)
    {
        _categoryRepository = categoriaRepository;
        _auditService = auditService;
        _validator = validator;
    }

    public async Task<Guid> CreateAsync(CategoriaRequestDto dto, Guid userId)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new BusinessException(errors);
        }
        
        if (await _categoryRepository.ExistsAsync(dto.Nome))
            throw new BusinessException("Já existe uma categoria com esse nome.");
        
        var category = new CategoriaModel(dto.Nome, dto.Descricao);
        
        await _categoryRepository.AddAsync(category);

        var auditLogDto = new AuditLogDto
        (
            Guid.NewGuid(),
            "CATEGORIA_CREATED",
            new { category.Id, category.Nome },
             userId,
             DateTime.UtcNow
            );
            
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
    
    public async Task UpdateAsync(Guid id, CategoriaUpdateDto dto, Guid userId)
    {
        var categoria = await _categoryRepository.GetByIdAsync(id)
                        ?? throw new NotFoundException("Categoria não encontrada.");
        
        if (!categoria.Nome.Equals(dto.Nome, StringComparison.OrdinalIgnoreCase))
        {
            if (await _categoryRepository.ExistsAsync(dto.Nome))
                throw new BusinessException($"Já existe uma categoria cadastrada com o nome '{dto.Nome}'.");
        }
        
        categoria.UpdateCategory(dto.Nome, dto.Descricao);
        
        await _categoryRepository.UpdateAsync(categoria);
        
        await _auditService.AuditLogAsync(new AuditLogDto(
            Guid.NewGuid(),
            "CATEGORIA_UPDATED",
            new { categoria.Id, categoria.Nome, AntigoNome = categoria.Nome },
            userId,
            DateTime.UtcNow
        ));
    }

    public async Task ActivateAsync(Guid categoriaId, Guid userId)
    {
        var categoria = await _categoryRepository.GetByIdAsync(categoriaId)
                        ?? throw new NotFoundException("Categoria não encontrada.");
        
        categoria.Ativar();
        await _categoryRepository.UpdateAsync(categoria);
        
        var auditLogDto = new AuditLogDto
        (
            Guid.NewGuid(),
            "CATEGORY_ACTIVE",
            new { categoria.Id, categoria.Nome },
             userId,
             DateTime.UtcNow
        );
        await _auditService.AuditLogAsync(auditLogDto);
    }

    public async Task DesactivateAsync(Guid categoryId, Guid userId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId)
                       ?? throw new NotFoundException("Categoria não encontrada.");

        category.Desativar();
        await _categoryRepository.UpdateAsync(category);

        var auditLogDto = new AuditLogDto
        (
            Guid.NewGuid(),
            "CATEGORY_DISABLE",
            new { category.Id, category.Nome },
            userId,
            DateTime.UtcNow);
            

        await _auditService.AuditLogAsync(auditLogDto);
    }
}