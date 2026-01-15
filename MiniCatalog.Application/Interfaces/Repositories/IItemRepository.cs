using MiniCatalog.Application.DTOs.Item;
using MiniCatalog.Domain.Common;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Interfaces.Repositories;

public interface IItemRepository
{
    Task<ItemModel?> GetByIdAsync(Guid id);
    Task<IEnumerable<ItemModel>> GetAllAsync();
    Task<bool> ExistsAsync(string nome, Guid categoryId);
    Task AddAsync(ItemModel item);
    Task UpdateAsync(ItemModel item);
    Task<(IEnumerable<ItemModel> Items, int Total, decimal Average)> SearchAdvancedAsync(
        string? term, Guid? categoriaId, decimal? min, decimal? max, 
        bool? ativo, string[]? tags, string sort, int page, int pageSize);
    
    Task<bool> ExistsByNameAndCategoryAsync(string nome, Guid categoriaId);
    Task<IEnumerable<ItemModel>> GetAllWithCategoryAsync();
}