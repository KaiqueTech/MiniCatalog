namespace MiniCatalog.Application.DTOs.Auth;

public record AuthResponseDto(bool Success, string Message, string? Token = null);