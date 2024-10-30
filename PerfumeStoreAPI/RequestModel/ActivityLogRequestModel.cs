namespace PerfumeStore.API.RequestModel
{
    public class ActivityLogRequestModel
    {
        public Guid PerfumeId { get; set; }

        public string? Action { get; set; }

        public string? Notes { get; set; }
    }
}
