using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCatalog.Application.DTOs.Item;
using MiniCatalog.Application.DTOs.Search;
using MiniCatalog.Application.Services;
using MiniCatalog.Domain.Constants;

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
    [Authorize(Policy = Policies.Editor)] 
    public async Task<IActionResult> Create(ItemRequestDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!); 
        var id = await _service.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Policies.Viewer)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null)
            return NotFound();

        return Ok(item);
    }
    
    [HttpGet()]
    [Authorize(Policy = Policies.Viewer)]
    public async Task<IActionResult> GetAllAsync()
    {
        var itemResult = await _service.GetAllAsync();
        if (itemResult == null!) return NotFound();
        return Ok(itemResult);
    }

    [HttpGet("search")]
    [Authorize(Policy = Policies.Viewer)]
    public async Task<IActionResult> GetSearch([FromQuery] SearchFilterDto filters)
    {
        return Ok(await _service.SearchAsync(filters));
    }

    [HttpPut("{id}/activate")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Activate(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _service.ActivateAsync(id, userId);
        return Ok(new { Message = "Item ativado com sucesso." });
    }

    [HttpPut("{id}/deactivate")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _service.DesableAsync(id, userId);
        return Ok(new { Message = "Item desativado com sucesso." });
    }
}