using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Perfume_Store_Activity_Management_Backend.src.Application.DTOs.Perfume;
using Perfume_Store_Activity_Management_Backend.src.Application.Services;
using Perfume_Store_Activity_Management_Backend.src.Infrastructure.Repository;

namespace Perfume_Store_Activity_Management_Backend.src.Application.Controllers;


[ApiController]
[Route("api/v1/perfumes")]
public class PerfumeController : ControllerBase
{
    private readonly IPerfumeService _perfumeService;
    private readonly ILogger _logger;

    public PerfumeController(IPerfumeService perfumeService, ILogger<PerfumeController> logger)
    {
        _logger = logger;
        _perfumeService = perfumeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPerfumes([FromQuery] GetPerfumeQuery req)
    {
        _logger.LogInformation("Get Perfume");
        return await _perfumeService.GetAllPerfumes(req);
    }
}
