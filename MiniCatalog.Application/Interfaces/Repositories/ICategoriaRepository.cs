using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Interfaces.Repositories;

public interface ICategoriaRepository
{
    Task<CategoriaModel?> GetByIdAsync(Guid id);
    Task<IEnumerable<CategoriaModel>> GetAllAsync();
    Task<bool> ExistsAsync(string nome);
    Task<CategoriaModel?> GetByNameAsync(string nome);
    Task AddAsync(CategoriaModel category);
    Task UpdateAsync(CategoriaModel category);
}