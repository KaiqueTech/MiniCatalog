using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCatalog.Application.DTOs.Import;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Domain.Constants;

namespace MiniCatalog.Api.Controllers.Import;

[ApiController]
[Route("[controller]")]
public class ImportController : ControllerBase
{
    private readonly IImportService _importService;

    public ImportController(IImportService importService)
    {
        _importService = importService;
    }
    
    [HttpPost("external")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<ActionResult<ImportResultDto>> ImportFromExternalApi()
    {
        // 1. Extrai o UserId do Token (Claim NameIdentifier)
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
        {
            return Unauthorized("Usuário não identificado ou ID inválido.");
        }
        
        var result = await _importService.ImportFromExternalApiAsync(userId);

        return Ok(result);
    }
}