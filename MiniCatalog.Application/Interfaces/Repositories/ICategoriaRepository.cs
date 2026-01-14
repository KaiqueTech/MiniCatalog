using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Interfaces.Repositories;

public interface ICategoriaRepository
{
    Task<CategoriaModel?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(string nome);
    Task AddAsync(CategoriaModel category);
    Task UpdateAsync(CategoriaModel category);
}