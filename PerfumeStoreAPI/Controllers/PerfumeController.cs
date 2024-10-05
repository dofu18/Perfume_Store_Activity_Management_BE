using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.API.RequestModel;
using PerfumeStore.API.ResponseModel;
using PerfumeStore.Repository.Models;
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

        [HttpGet("/perfume/{id}")]
        public async Task <ActionResult<IEnumerable<PerfumeResponseModel>>> GetPerfumeById(Guid id)
        {
            var perfume = await _perfumeService.GetPerfumeByIdAsync(id);
            if (perfume == null) return NotFound();

            var response = new PerfumeResponseModel
            {
                PerfumeId = perfume.PerfumeId,
                ViewCount = perfume.ViewCount,
                Origin = perfume.Origin,
                ReleaseYear = perfume.ReleaseYear,
                Concentration = perfume.Concentration,
                Bartender = perfume.Bartender,
                FlavorGroup = perfume.FlavorGroup,
                Capacity = perfume.Capacity,
                Price = perfume.Price,
                Discount = perfume.Discount,
                TopNote = perfume.TopNote,
                MiddleNote = perfume.MiddleNote,
                BaseNote = perfume.BaseNote,
                PerfumeEditions = perfume.PerfumeEditions,
            };

            return Ok(response);
        }

        [HttpPost("perfume")]
        public async Task <ActionResult> CreatePerfume(PerfumeRequestModel req)
        {
            var perfumeModel = new PerfumeModel
            {
                ViewCount = req.ViewCount,
                Origin = req.Origin,
                ReleaseYear = req.ReleaseYear,
                Concentration = req.Concentration,
                Bartender = req.Bartender,
                FlavorGroup = req.FlavorGroup,
                Capacity = req.Capacity,
                Price = req.Price,
                Discount = req.Discount,
                TopNote = req.TopNote,
                MiddleNote = req.MiddleNote,
                BaseNote = req.BaseNote,
                //PerfumeEditions = req.PerfumeEditions,
            };

            var rs = await _perfumeService.InsertPerfumeAsync(perfumeModel);
            perfumeModel.PerfumeId = rs;
            return CreatedAtAction(nameof(GetPerfumeById), new {id = perfumeModel.PerfumeId}, perfumeModel );
        }

        [HttpPut("perfume/{id}")]
        public async Task<IActionResult> UpdatePerfume (Guid id, PerfumeRequestModel req)
        {
            var perfumeModel = new PerfumeModel
            {
                ViewCount = req.ViewCount,
                Origin = req.Origin,
                ReleaseYear = req.ReleaseYear,
                Concentration = req.Concentration,
                Bartender = req.Bartender,
                FlavorGroup = req.FlavorGroup,
                Capacity = req.Capacity,
                Price = req.Price,
                Discount = req.Discount,
                TopNote = req.TopNote,
                MiddleNote = req.MiddleNote,
                BaseNote = req.BaseNote,
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
