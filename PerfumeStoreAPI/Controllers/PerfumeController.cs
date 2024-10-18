using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.API.RequestModel;
using PerfumeStore.API.ResponseModel;
using PerfumeStore.Repository.Model;
using PerfumeStore.Service.BusinessModel;
using PerfumeStore.Service.Service;
using System.Formats.Asn1;
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
        [Authorize]
        public IActionResult GetPerfume([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] bool desc, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // Get paginated result from the service
            var perfumes = _perfumeService.GetPerfumes(search, sortBy, desc, page, pageSize);
            return Ok(perfumes);
        }

        [HttpGet("/perfume/{id}")]
        public async Task <ActionResult<IEnumerable<PerfumeResponseModel>>> GetPerfumeById(Guid id)
        {
            var perfume = await _perfumeService.GetPerfumeByIdAsync(id);
            if (perfume == null) return NotFound();

            var response = new PerfumeResponseModel
            {
                PerfumeId = perfume.PerfumeId,
                Name = perfume.Name,
                Brand = perfume.Brand,
                Scent = perfume.Scent,
                Gender = perfume.Gender,
                StockQuantity = perfume.StockQuantity,
                Description = perfume.Description,
                ImageUrl = perfume.ImageUrl,
                ViewCount = perfume.ViewCount,
                Origin = perfume.Origin,
                ReleaseYear = perfume.ReleaseYear,
                Volume = perfume.Volume,
                Price = perfume.Price,
                Discount = perfume.Discount,
                TopNote = perfume.TopNote,
                MiddleNote = perfume.MiddleNote,
                BaseNote = perfume.BaseNote,
                DateAdded = perfume.DateAdded,
            };

            return Ok(response);
        }

        [HttpPost("perfume")]
        public async Task <ActionResult> CreatePerfume(PerfumeRequestModel req)
        {
            var perfumeModel = new PerfumeProductModel
            {
                PerfumeId = req.PerfumeId,
                Name = req.Name,
                Brand = req.Brand,
                Scent = req.Scent,
                Gender = req.Gender,
                StockQuantity = req.StockQuantity,
                Description = req.Description,
                ImageUrl = req.ImageUrl,
                ViewCount = req.ViewCount,
                Origin = req.Origin,
                ReleaseYear = req.ReleaseYear,
                Volume = req.Volume,
                Price = req.Price,
                Discount = req.Discount,
                TopNote = req.TopNote,
                MiddleNote = req.MiddleNote,
                BaseNote = req.BaseNote,
                DateAdded = req.DateAdded,
            };

            var rs = await _perfumeService.InsertPerfumeAsync(perfumeModel);
            perfumeModel.PerfumeId = rs;
            return CreatedAtAction(nameof(GetPerfumeById), new {id = perfumeModel.PerfumeId}, perfumeModel );
        }

        [HttpPut("perfume/{id}")]
        public async Task<IActionResult> UpdatePerfume (Guid id, PerfumeRequestModel req)
        {
            var perfumeModel = new PerfumeProductModel
            {
                PerfumeId = req.PerfumeId,
                Name = req.Name,
                Brand = req.Brand,
                Scent = req.Scent,
                Gender = req.Gender,
                StockQuantity = req.StockQuantity,
                Description = req.Description,
                ImageUrl = req.ImageUrl,
                ViewCount = req.ViewCount,
                Origin = req.Origin,
                ReleaseYear = req.ReleaseYear,
                Volume = req.Volume,
                Price = req.Price,
                Discount = req.Discount,
                TopNote = req.TopNote,
                MiddleNote = req.MiddleNote,
                BaseNote = req.BaseNote,
                DateAdded = req.DateAdded,
            };

            var success = await _perfumeService.UpdatePerfumeAsync(id, perfumeModel);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("perfume/{id}")]
        public async Task<IActionResult> DeletePerfume (Guid id)
        {
            var success = await _perfumeService.DeletePerfumeAsync(id);
            if (!success ) return NotFound();

            return NoContent();
        }
    }
}
