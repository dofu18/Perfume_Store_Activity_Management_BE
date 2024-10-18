namespace PerfumeStore.API.ResponseModel
{
    public class UserResponseModel
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? ProfileUrl { get; set; }
        public string? Metadata { get; set; }
        public string? Status { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
