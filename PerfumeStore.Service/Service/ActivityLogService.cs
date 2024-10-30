using PerfumeStore.Repository.Model;
using PerfumeStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PerfumeStore.Service.BusinessModel;
using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Service.BusinessModel.CustomResponse;

namespace PerfumeStore.Service.Service
{
    public class ActivityLogService
    {
        private readonly UnitOfWork _unitOfWork;

        public ActivityLogService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ActivityLog>> GetActivityByUserIdAsync(Guid userId)
        {
            return await _unitOfWork.ActivityLogs
                .FindByCondition(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> InsertActivityLogAsync(Guid userId, ActivityLogModel activityLogModel)
        {
            var activityLog = new ActivityLog
            {
                ActivityId = Guid.NewGuid(),  // Assuming ActivityID is a GUID
                UserId = userId,
                PerfumeId = activityLogModel.PerfumeId,
                Action = activityLogModel.Action,
                Date = DateTime.UtcNow,
                Notes = activityLogModel.Notes,
            };

            // Add the new activity log entry to the repository
            await _unitOfWork.ActivityLogs.CreateAsync(activityLog);

            // Save changes to the database
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteActivityLogAsync(Guid activityId, Guid userId)
        {
            var activityLogList = await GetActivityByUserIdAsync(userId);

            var activityLogToDelete = await _unitOfWork.ActivityLogs.GetByIdAsync(activityId);
            if (activityLogToDelete == null) return false;

            if (activityLogList.Any(a => a.ActivityId == activityLogToDelete.ActivityId))
            {
                _unitOfWork.ActivityLogs.Remove(activityLogToDelete);
                await _unitOfWork.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<IActionResult> UpdateActivityLogAsync(Guid id, string categoryName)
        {
            try
            {
                var categoryToUpdate = await _unitOfWork.Category.GetByIdAsync(id);
                if (categoryToUpdate == null) return ErrorResp.NotFound("Category not found");

                categoryToUpdate.CategoryName = categoryName;

                _unitOfWork.Category.Update(categoryToUpdate);

                await _unitOfWork.SaveAsync();
                return SuccessResp.Ok("Update Category Successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
