using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Interfaces.Repositories;

public interface IItemRepository
{
    Task<ItemModel?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(string nome, Guid categoryId);
    Task AddAsync(ItemModel item);
    Task UpdateAsync(ItemModel item);
}