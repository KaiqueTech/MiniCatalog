using Microsoft.EntityFrameworkCore;
using MiniCatalog.Application.DTOs.Audit;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Domain.Models;
using MiniCatalog.Infra.Persistence.Context;

namespace MiniCatalog.Infra.Persistence.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<CategoriaModel?> GetByIdAsync(Guid id)
    {
        var resultado = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        return resultado;
    }

    public async Task<bool> ExistsAsync(string nome)
    {
        return await _context.Categorias.AnyAsync(c => c.Nome == nome);
    }

    public async Task AddAsync(CategoriaModel categoria)
    {
       await _context.Categorias.AddAsync(categoria);
       await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CategoriaModel categoria)
    {
         _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();
    }
}