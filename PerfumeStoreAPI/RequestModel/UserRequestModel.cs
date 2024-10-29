namespace PerfumeStore.API.RequestModel
{
    public class UserRequestModel
    {

        public string? FirstName { get; set; } = null!;

        public string? LastName { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? ProfileUrl { get; set; }

        public string? Metadata { get; set; }
    }
}
