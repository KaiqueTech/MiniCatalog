using FluentValidation;
using MiniCatalog.Application.DTOs.Audit;
using MiniCatalog.Application.DTOs.Item;
using MiniCatalog.Application.DTOs.Search;
using MiniCatalog.Application.Exceptions;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Services;

public class ItemService
{
    private readonly IItemRepository _itemRepo;
    private readonly ICategoriaRepository _categoryRepo;
    private readonly IAuditService _auditService;
    private readonly IValidator<ItemRequestDto> _validator;

    public ItemService(IItemRepository itemRepo, ICategoriaRepository categoryRepo,
        IAuditService auditService, IValidator<ItemRequestDto> validator)
    {
        _itemRepo = itemRepo;
        _categoryRepo = categoryRepo;
        _auditService = auditService;
        _validator = validator;
        
    }

    public async Task<Guid> CreateAsync(ItemRequestDto dto, Guid userId)
    {
        var validationResult = await _validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new BusinessException(errors);
        }
        var categoria = await _categoryRepo.GetByIdAsync(dto.CategoriaId)
                       ?? throw new NotFoundException("Categoria não encontrada");

        if (await _itemRepo.ExistsAsync(dto.Nome, dto.CategoriaId))
            throw new BusinessException("Item duplicado");

        var item = new ItemModel(
            dto.Nome,
            dto.Descricao,
            dto.CategoriaId,
            dto.Preco
        );

        foreach (var tag in dto.Tags)
            item.AdicionarTag(tag);

        await _itemRepo.AddAsync(item);
        
        await _auditService.AuditLogAsync(new AuditLogDto
        (
             Guid.NewGuid(), 
             "ITEM_CADASTRADO",
            new { item.Id, item.Nome }, 
             userId,
            DateTime.UtcNow
        ));

        return item.Id;
    }

    public async Task<ItemResponseDto> GetByIdAsync(Guid itemId)
    {
        var item = await _itemRepo.GetByIdAsync(itemId);
        if (item == null)
            throw new NotFoundException("Item não encontrado.");

        return new ItemResponseDto(
            item.Id,
            item.Nome,
            item.Descricao,
            item.Preco,
            item.Categoria.Nome,
            item.Tags.Select(t => t.Tag).ToList(),
            item.Ativo,
            item.CreatedAt
            );
    }

    public async Task<IEnumerable<ItemResponseDto>> GetAllAsync()
    {
        var items = await _itemRepo.GetAllAsync();
        
        return items.Select(item => new ItemResponseDto(
            item.Id,                                     
            item.Nome,                  
            item.Descricao,               
            item.Preco,           
            item.Categoria?.Nome ?? "Sem Categoria",
            item.Tags.Select(t => t.Tag).ToList(),
            item.Ativo,
            item.CreatedAt
        ));
    }

    public async Task ActivateAsync(Guid itemId, Guid userId)
    {
        var item = await _itemRepo.GetByIdAsync(itemId)
                   ?? throw new NotFoundException("Item não encontrado");

        item.Active();
        await _itemRepo.UpdateAsync(item);

        await _auditService.AuditLogAsync(new AuditLogDto
        (
            Guid.NewGuid(),
            "ITEM_ATIVADO",
           new { item.Id, item.Nome }, 
            userId,
            DateTime.UtcNow
        ));
    }

    public async Task DesableAsync(Guid itemId, Guid userId)
    {
        var item = await _itemRepo.GetByIdAsync(itemId)
                   ?? throw new NotFoundException("Item não encontrado");

        item.Disable();
        await _itemRepo.UpdateAsync(item);

        await _auditService.AuditLogAsync(new AuditLogDto
        (
            Guid.NewGuid(),
            "ITEM_DESATIVADO",
            new { item.Id, item.Nome },
            userId,
            DateTime.UtcNow
        ));
    }
    public async Task<SearchResultDto> SearchAsync(SearchFilterDto filterDto)
    {
            var tagList = filterDto.Tags?.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            var (entities, total, average) = await _itemRepo.SearchAdvancedAsync(
                filterDto.Term, filterDto.CategoriaId, filterDto.Min, filterDto.Max, filterDto.Ativo, tagList, filterDto.Sort ?? "nome", filterDto.Page, filterDto.PageSize);
        
            var itemsDto = entities.Select(i => new ItemResponseDto(
                i.Id,
                i.Nome,
                i.Descricao,
                i.Preco,
                i.Categoria.Nome,
                i.Tags.Select(t => t.Tag).ToList(),
                i.Ativo,
                i.CreatedAt
            ));

            return new SearchResultDto(
                itemsDto,
                total,
                average,
                filterDto.Page,
                (int)Math.Ceiling((double)total / filterDto.PageSize)
            );
    }
}