using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCatalog.Application.DTOs.Categoria;
using MiniCatalog.Application.Services;
using MiniCatalog.Domain.Constants;

namespace MiniCatalog.Api.Controllers.Categoria;

[ApiController]
[Route("api/categorias")]
[Authorize]
public class CategoriaController : ControllerBase
{
    private readonly CategoriaService _service;

    public CategoriaController(CategoriaService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Policy = Policies.Editor)]
    public async Task<IActionResult> Create(CategoriaRequestDto dto)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized();
        
        var id = await _service.CreateAsync(dto, userId);
        return Created($"api/categorias/{id}", id);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Policies.Viewer)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var categoriaResult = await _service.GetByIdAsync(id);
        if (categoriaResult == null) return NotFound();
        return Ok(categoriaResult);
    }

    [HttpGet()]
    [Authorize(Policy = Policies.Viewer)]
    public async Task<IActionResult> GetAllAsync()
    {
        var categoriasResult = await _service.GetAllAsync();
        if (categoriasResult == null) return NotFound();
        return Ok(categoriasResult);
    }

    [HttpPut("{id}/activate")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Activate(Guid id)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized();
        
        await _service.ActivateAsync(id, userId);
        return Ok(new { Message = "Categoria ativada com sucesso." });
    }
    
    [HttpPut("{id}/deactivate")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized();
        
        await _service.DesactivateAsync(id, userId);
        return Ok(new { Message = "Categoria desativada com sucesso." });
    }
}