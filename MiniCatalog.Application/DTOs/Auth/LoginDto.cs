using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniCatalog.Application.DTOs.Auth;

public record LoginDto(string Email, string Password);