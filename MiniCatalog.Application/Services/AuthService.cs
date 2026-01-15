using Microsoft.AspNetCore.Identity;
using MiniCatalog.Application.DTOs.Auth;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Domain.Enums;
using MiniCatalog.Domain.Models;

namespace MiniCatalog.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto request)
    {
        var identityUser = new IdentityUser { UserName = request.UserName, Email = request.Email };
        var result = await _userManager.CreateAsync(identityUser, request.Password);

        if (!result.Succeeded)
            return new AuthResponseDto(false, string.Join(", ", result.Errors.Select(e => e.Description)));
        
        var userDomain = new UserModel(
            request.Email,
            request.UserName,
            request.dateOfBirth,
            identityUser.Id 
        );
        await _userRepository.CreateUserAsync(userDomain);
        
        string roleName = request.Role.ToString();
        
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        
        await _userManager.AddToRoleAsync(identityUser, roleName);
        
        var token = await _tokenService.GenerateTokenAsync(identityUser);

        return new AuthResponseDto(true, "Usuário registrado com sucesso", token);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto request)
    {
        var identityUser = await _userManager.FindByEmailAsync(request.Email);
        
        if (identityUser == null || !await _userManager.CheckPasswordAsync(identityUser, request.Password))
            return new AuthResponseDto(false, "Credenciais inválidas");
        
        var token = await _tokenService.GenerateTokenAsync(identityUser);
        return new AuthResponseDto(true, "Login realizado com sucesso", token);
    }

    public async Task<UserViewDto?> GetMeAsync(string email)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);
        var roles = await _userManager.GetRolesAsync(identityUser!);
        
        var roleString = roles.FirstOrDefault() ?? nameof(UserRole.Viewer);
        Enum.TryParse(roleString, out UserRole roleEnum);

        var user = await _userRepository.GetByEmailAsync(email);

        return new UserViewDto(
            Nome: user.UserName,
            Email: user.Email,
            Role: roleEnum,
            DateOfBirth: user.DateOfBirth
        );
    }
}