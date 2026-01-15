using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniCatalog.Application.Interfaces.Services;
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
        var (content, fileName) = await reportService.ExportItemsToCsvAsync();
        return File(content, "text/csv", fileName);
    }
}