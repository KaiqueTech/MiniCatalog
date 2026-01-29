using Microsoft.EntityFrameworkCore;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Domain.Models;
using MiniCatalog.Infra.Persistence.Context;

namespace MiniCatalog.Infra.Persistence.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;

    public ItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ItemModel?> GetByIdAsync(Guid id)
    {
        return await _context.Items
            .Include(i => i.Categoria)
            .Include(i => i.Tags)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<ItemModel>> GetAllAsync()
    {
        return await _context.Items
            .Include(i => i.Categoria)
            .Include(i => i.Tags)
            .AsNoTracking().ToListAsync();
    }

    public async Task<bool> ExistsAsync(string nome, Guid categoriaId)
    {
        return await _context.Items.AnyAsync(i => i.Nome == nome && i.CategoriaId == categoriaId);
    }

    public async Task AddAsync(ItemModel item)
    {
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ItemModel item)
    {
        _context.Items.Update(item);
        await _context.SaveChangesAsync();
    }
    
    public async Task<(IEnumerable<ItemModel> Items, int Total, decimal Average)> SearchAdvancedAsync(
        string? term, Guid? categoriaId, decimal? min, decimal? max, 
        bool? ativo, string[]? tags, string sort, int page, int pageSize)
    {
        var query = _context.Items
            .Include(i => i.Categoria)
            .Include(i => i.Tags)
            .AsNoTracking()
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(term))
            query = query.Where(i => i.Nome.Contains(term) || i.Descricao!.Contains(term));

        if (categoriaId.HasValue) query = query.Where(i => i.CategoriaId == categoriaId);
        if (min.HasValue) query = query.Where(i => i.Preco >= min.Value);
        if (max.HasValue) query = query.Where(i => i.Preco <= max.Value);
        if (ativo.HasValue) query = query.Where(i => i.Ativo == ativo.Value);

        if (tags != null && tags.Any())
            query = query.Where(i => i.Tags.Any(t => tags.Contains(t.Tag)));
        
        var total = await query.CountAsync();
        var average = total > 0 ? await query.AverageAsync(i => i.Preco) : 0;
        
        query = sort.ToLower() switch {
            "preco" => query.OrderBy(i => i.Preco),
            "data" => query.OrderByDescending(i => i.CreatedAt),
            _ => query.OrderBy(i => i.Nome)
        };
        
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, total, average);
    }

    public async Task<IEnumerable<ItemModel>> GetAllWithCategoryAsync()
    {
        return await _context.Items
            .Include(i => i.Categoria)
            .Include(i => i.Tags)
            .AsNoTracking()
            .ToListAsync();
    }
}