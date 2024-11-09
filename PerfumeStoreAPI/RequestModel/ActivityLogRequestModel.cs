using PerfumeStore.Repository.Enum;

namespace PerfumeStore.API.RequestModel
{
    public class ActivityLogRequestModel
    {
        public Guid PerfumeId { get; set; }

        public ActionEnum? Action { get; set; }

        public string? Notes { get; set; }
    }
}
