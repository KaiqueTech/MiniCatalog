using MiniCatalog.Application.DTOs.Auth;

namespace MiniCatalog.Application.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto request);
    Task<AuthResponseDto> LoginAsync(LoginDto request);
    Task<UserViewDto?> GetMeAsync(string email);
}