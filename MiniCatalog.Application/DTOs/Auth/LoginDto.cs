using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MiniCatalog.Application.DTOs.Auth;

public record LoginDto([Required][EmailAddress]string Email, [Required][PasswordPropertyText]string Password);