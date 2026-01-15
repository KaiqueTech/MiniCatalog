namespace MiniCatalog.Application.DTOs.Auth;

public record UserUpdateDto(string NovoEmail, string SenhaAtual, string NovaSenha);