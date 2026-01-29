using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Application.Settings;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Infra.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public TokenService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings;
    }

    public async Task<string> GenerateTokenAsync(IdentityUser identity)
    {
        var roles = await _userManager.GetRolesAsync(identity);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, identity.Id), 
            new Claim(ClaimTypes.Name, identity.UserName ?? ""),
            new Claim(ClaimTypes.Role, roles.FirstOrDefault()!),
            new Claim(JwtRegisteredClaimNames.Email, identity.Email ?? "")
        };

        //claims.AddRange(roles.Select(r => new Claim("role", r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpiresInHours),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}