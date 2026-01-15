using Microsoft.EntityFrameworkCore;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Domain.Models;
using MiniCatalog.Infra.Persistence.Context;

namespace MiniCatalog.Infra.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateUserAsync(UserModel user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserModel?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserModel?> GetByIdentityIdAsync(string identityId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.IdentityId == identityId);
    }
}