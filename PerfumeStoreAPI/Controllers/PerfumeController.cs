using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.API.ResponseModel;
using PerfumeStore.Repository.Models;
using PerfumeStore.Service.Service;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PerfumeStore.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class PerfumeController : ControllerBase
    {
        private readonly PerfumeService _perfumeService;

        public PerfumeController(PerfumeService perfumeService)
        {
            _perfumeService = perfumeService;
        }

        [HttpGet("perfumes")]
        public async Task<ActionResult<IEnumerable<PerfumeResponseModel>>> GetPerfume()
        {
            var perfumes = await _perfumeService.GetPerfumesAsync();
            var response = perfumes.Select(perfumes => new PerfumeResponseModel
            {
                PerfumeId = perfumes.PerfumeId,
                ViewCount = perfumes.ViewCount,
                Origin = perfumes.Origin,
                ReleaseYear = perfumes.ReleaseYear,
                Concentration = perfumes.Concentration,
                Bartender = perfumes.Bartender,
                FlavorGroup = perfumes.FlavorGroup,
                Capacity = perfumes.Capacity,   
                Price = perfumes.Price,
                Discount = perfumes.Discount,
                TopNote = perfumes.TopNote,
                MiddleNote = perfumes.MiddleNote,
                BaseNote = perfumes.BaseNote,
                PerfumeEditions = perfumes.PerfumeEditions,
            });

            return Ok(response);
        }
    }
}
