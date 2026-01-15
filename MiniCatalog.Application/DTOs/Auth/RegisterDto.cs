using MiniCatalog.Domain.Enums;

namespace MiniCatalog.Application.DTOs.Auth;

public record RegisterDto(string UserName,string Email, string Password, UserRole Role, DateOnly DateOfBirth);