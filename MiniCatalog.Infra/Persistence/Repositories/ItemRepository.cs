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
}