using Microsoft.AspNetCore.Identity;

namespace MiniCatalog.Application.Interfaces.Services;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(IdentityUser identity);
}