using MiniCatalog.Domain.Enums;

namespace MiniCatalog.Application.DTOs.Auth;

public record UserViewDto(string Nome,string Email, UserRole Role,  DateOnly DateOfBirth);