using Microsoft.AspNetCore.Mvc;
using MiniCatalog.Application.DTOs.Categoria;
using MiniCatalog.Application.Services;

namespace MiniCatalog.Api.Controllers.Categoria;

[ApiController]
[Route("api/categorias")]
public class CategoriaController : ControllerBase
{
    private readonly CategoriaService _service;

    public CategoriaController(CategoriaService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoriaRequestDto dto)
    {
        var userId = Guid.NewGuid(); // substituir pelo JWT real
        var id = await _service.CreateAsync(dto, userId);
        return Created($"api/categorias/{id}", id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var categoriaResult = await _service.GetByIdAsync(id);
        if (categoriaResult == null) return NotFound();
        return Ok(categoriaResult);
    }

    [HttpPut("{id}/activate")]
    public async Task<IActionResult> Activate(Guid id)
    {
        var userId = Guid.NewGuid();
        await _service.ActivateAsync(id, userId);
        return Ok(new { Message = "Categoria ativada com sucesso." });
    }
    
    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var userId = Guid.NewGuid();
        await _service.DesactivateAsync(id, userId);
        return Ok(new { Message = "Categoria desativada com sucesso." });
    }
}