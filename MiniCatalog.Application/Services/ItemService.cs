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

    public ItemService(
        IItemRepository itemRepo,
        ICategoriaRepository categoryRepo,
        IAuditService auditService)
    {
        _itemRepo = itemRepo;
        _categoryRepo = categoryRepo;
        _auditService = auditService;
    }

    public async Task<Guid> CreateAsync(ItemRequestDto dto, Guid userId)
    {
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
        {
            LogId = Guid.NewGuid(),
            Action = "ITEM_CADASTRADO",
            Payload = new { item.Id, item.Nome },
            UserId = userId,
            Timestamp = DateTime.UtcNow
        });

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
            item.Ativo
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
            item.Ativo                                   
        ));
    }

    public async Task AtivarAsync(Guid itemId, Guid userId)
    {
        var item = await _itemRepo.GetByIdAsync(itemId)
                   ?? throw new NotFoundException("Item não encontrado");

        item.Ativar();
        await _itemRepo.UpdateAsync(item);

        await _auditService.AuditLogAsync(new AuditLogDto
        {
            LogId = Guid.NewGuid(),
            Action = "ITEM_ATIVADO",
            Payload = new { item.Id, item.Nome },
            UserId = userId,
            Timestamp = DateTime.UtcNow
        });
    }

    public async Task DesativarAsync(Guid itemId, Guid userId)
    {
        var item = await _itemRepo.GetByIdAsync(itemId)
                   ?? throw new NotFoundException("Item não encontrado");

        item.Desativar();
        await _itemRepo.UpdateAsync(item);

        await _auditService.AuditLogAsync(new AuditLogDto
        {
            LogId = Guid.NewGuid(),
            Action = "ITEM_DESATIVADO",
            Payload = new { item.Id, item.Nome },
            UserId = userId,
            Timestamp = DateTime.UtcNow
        });
    }
    public async Task<SearchResultDto> SearchAsync(SearchFilterDto filterDto)
    {
        var tagList = filterDto.Tags?.Split(',', StringSplitOptions.RemoveEmptyEntries).ToArray();

        var (entities, total, average) = await _itemRepo.SearchAdvancedAsync(
            filterDto.Term, filterDto.CategoriaId, filterDto.Min, filterDto.Max, filterDto.Ativo, tagList, filterDto.Sort ?? "nome", filterDto.Page, filterDto.PageSize);
        
        var itemsDto = entities.Select(i => new ItemResponseDto(
            i.Id,
            i.Nome,
            i.Descricao,
            i.Preco,
            i.Categoria.Nome,
            i.Tags.Select(t => t.Tag).ToList(),
            i.Ativo
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