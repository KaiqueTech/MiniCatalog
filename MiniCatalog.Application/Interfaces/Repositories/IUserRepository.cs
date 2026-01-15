using Microsoft.AspNetCore.Identity;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task CreateUserAsync(UserModel user); 
    
    Task<UserModel?> GetByEmailAsync(string email);
    
    Task<UserModel?> GetByIdentityIdAsync(string identityId);
}