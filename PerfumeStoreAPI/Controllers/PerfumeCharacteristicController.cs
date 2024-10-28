using Microsoft.AspNetCore.Mvc;
using PerfumeStore.API.RequestModel;
using PerfumeStore.API.ResponseModel;
using PerfumeStore.Service.BusinessModel;
using PerfumeStore.Service.Service;

namespace PerfumeStore.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class PerfumeCharacteristicController : ControllerBase
    {
        private readonly PerfumeCharacteristicService _perfumeCharacteristicService;

        public PerfumeCharacteristicController(PerfumeCharacteristicService perfumeCharacteristicService)
        {
            _perfumeCharacteristicService = perfumeCharacteristicService;
        }

        [HttpGet("characteristic")]
        public IActionResult GetPerfumeCharacteristic([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] bool desc, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // Get paginated result from the service
            var characteristics = _perfumeCharacteristicService.GetCharacteristic(search, sortBy, desc, page, pageSize);
            return Ok(characteristics);
        }

        [HttpGet("/characteristic/{id}")]
        public async Task<ActionResult<IEnumerable<CharacteristicResponseModel>>> GetCharacteristicById(Guid id)
        {
            var characteristic = await _perfumeCharacteristicService.GetCharacteristicByIdAsync(id);
            if (characteristic == null) return NotFound();

            var response = new CharacteristicResponseModel
            {
                CharacteristicId = characteristic.CharacteristicId,
                PerfumeId = characteristic.PerfumeId,
                AttributeName = characteristic.AttributeName,
                AttributeValue = characteristic.AttributeValue,
                LastUpdated = characteristic.LastUpdated,
            };

            return Ok(response);
        }

        [HttpPost("characteristic")]
        public async Task<ActionResult> CreateCharacteristic(CharacteristicRequestModel req)
        {
            var characteristicModel = new PerfumeCharacteristicModel
            {
                CharacteristicId = req.CharacteristicId,
                PerfumeId = req.PerfumeId,
                AttributeName= req.AttributeName,
                AttributeValue= req.AttributeValue,
            };

            var rs = await _perfumeCharacteristicService.InsertChacracteristicAsync(characteristicModel);
            characteristicModel.CharacteristicId = rs;
            return CreatedAtAction(nameof(GetCharacteristicById), new { id = characteristicModel.CharacteristicId }, characteristicModel);
        }

        [HttpPut("characteristic/{id}")]
        public async Task<IActionResult> UpdateCharacteristic(Guid id, CharacteristicRequestModel req)
        {
            var characteristicModel = new PerfumeCharacteristicModel
            {
                CharacteristicId = req.CharacteristicId,
                PerfumeId = req.PerfumeId,
                AttributeName = req.AttributeName,
                AttributeValue = req.AttributeValue,
            };

            var success = await _perfumeCharacteristicService.UpdateCharacteristicAsync(id, characteristicModel);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("characteristic/{id}")]
        public async Task<IActionResult> DeleteCharacteristic(Guid id)
        {
            var success = await _perfumeCharacteristicService.DeleteCharacteristicAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
