using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Domain.Constants;

namespace MiniCatalog.Api.Controllers.Report;

[ApiController]
[Route("api/reports")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }
    
    [HttpGet("items")]
    [Authorize(Policy = Policies.Viewer)]
    public async Task<IActionResult> GetReport()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        
        if (!Guid.TryParse(userIdString, out var userId))
            return Unauthorized();
        
        var (content, fileName) = await _reportService.ExportItemsToCsvAsync(userId);
        return File(content, "text/csv", fileName);
    }
}