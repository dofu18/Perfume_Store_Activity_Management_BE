using Microsoft.AspNetCore.Mvc;
using PerfumeStore.API.RequestModel;
using PerfumeStore.Service.BusinessModel;
using PerfumeStore.Service.Service;

namespace PerfumeStore.API.Controllers
{
    [Route("api/v1/[action]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        private readonly ActivityLogService _activityLogService;

        public ActivityLogController(ActivityLogService activityLogService)
        {
            _activityLogService = activityLogService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetActivityByUser(Guid userId)
        {
            var activities = await _activityLogService.GetActivityByUserIdAsync(userId);

            if (activities == null || !activities.Any())
            {
                return NotFound("Don't have any activity or user not found.");
            }

            return Ok(activities);
        }

        [HttpPost]
        public async Task<IActionResult> AddActivity(Guid userId, ActivityLogRequestModel req)
        {
            try
            {
                var activityModel = new ActivityLogModel
                {
                    PerfumeId = req.PerfumeId,
                    Action = req.Action,
                    Notes = req.Notes,
                };

                var success = await _activityLogService.InsertActivityLogAsync(userId, activityModel);

                return Ok("Item successfully added to cart.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteActivityLog(Guid userId, Guid activityLogId)
        {
            var success = await _activityLogService.DeleteActivityLogAsync(activityLogId, userId);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
