using FluentValidation;
using Microsoft.AspNetCore.Identity;
using MiniCatalog.Application.DTOs.Auth;
using MiniCatalog.Application.Exceptions;
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
    private readonly IValidator<LoginDto> _loginValidator;
    private readonly IValidator<RegisterDto> _registerValidator;

    public AuthService(
        IUserRepository userRepository,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService,
        IValidator<LoginDto> loginValidator,
        IValidator<RegisterDto> registerValidator)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _loginValidator = loginValidator;
        _registerValidator = registerValidator;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto request)
    {
        var validationResult = await _registerValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            
            return new AuthResponseDto(false, errors);
        }
        
        var identityUser = new IdentityUser { UserName = request.UserName, Email = request.Email };
        var result = await _userManager.CreateAsync(identityUser, request.Password);

        if (!result.Succeeded)
            return new AuthResponseDto(false, string.Join(", ", result.Errors.Select(e => e.Description)));
        
        var userDomain = new UserModel(
            request.Email,
            request.UserName,
            request.DateOfBirth,
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
        var validationResult = await _loginValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
            
            return new AuthResponseDto(false, errors);
        }
        
        var identityUser = await _userManager.FindByEmailAsync(request.Email);
        
        if (identityUser == null || !await _userManager.CheckPasswordAsync(identityUser, request.Password))
            return new AuthResponseDto(false, "Credenciais inválidas");
        
        var token = await _tokenService.GenerateTokenAsync(identityUser);
        return new AuthResponseDto(true, "Login realizado com sucesso", token);
    }

    public async Task<UserViewDto?> GetMeAsync(string email)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);
    
        if (identityUser == null)
        {
            throw new NotFoundException($"Usuário com email {email} não encontrado no Identity.");
        }
        
        var roles = await _userManager.GetRolesAsync(identityUser);
        var roleString = roles.FirstOrDefault() ?? nameof(UserRole.Viewer);
        Enum.TryParse(roleString, out UserRole roleEnum);
        
        var userDomain = await _userRepository.GetByEmailAsync(email);
        if (userDomain == null)
            throw new NotFoundException("Dados complementares do usuário não encontrados.");

        return new UserViewDto(
            Nome: userDomain.UserName,
            Email: userDomain.Email,
            Role: roleEnum,
            DateOfBirth: userDomain.DateOfBirth
        );
    }
}