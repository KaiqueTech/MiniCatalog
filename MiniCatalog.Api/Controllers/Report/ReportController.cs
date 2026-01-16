using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCatalog.Application.Services;
using MiniCatalog.Domain.Constants;

namespace MiniCatalog.Api.Controllers.Report;

[ApiController]
[Route("api/reports")]
public class ReportController(ReportService reportService) : ControllerBase
{
    [HttpGet("items")]
    [Authorize(Policy = Policies.Viewer)]
    public async Task<IActionResult> GetReport()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        
        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized();
        
        var (content, fileName) = await reportService.ExportItemsToCsvAsync(userId);
        return File(content, "text/csv", fileName);
    }
}