using Microsoft.AspNetCore.Mvc;
using MiniCatalog.Application.DTOs.Item;
using MiniCatalog.Application.Services;

namespace MiniCatalog.Api.Controllers.Item;

[ApiController]
[Route("api/items")]
public class ItemController : ControllerBase
{
    private readonly ItemService _service;

    public ItemController(ItemService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(ItemRequestDto dto)
    {
        var userId = Guid.NewGuid(); 
        var id = await _service.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null)
            return NotFound();

        return Ok(item);
    }
    
    [HttpPut("{id}/activate")]
    public async Task<IActionResult> Activate(Guid id)
    {
        var userId = Guid.NewGuid();
        await _service.AtivarAsync(id, userId);
        return Ok(new { Message = "Item ativado com sucesso." });
    }
    
    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var userId = Guid.NewGuid();
        await _service.DesativarAsync(id, userId);
        return Ok(new { Message = "Item desativado com sucesso." });
    }
}