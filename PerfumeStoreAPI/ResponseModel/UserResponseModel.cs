namespace PerfumeStore.API.ResponseModel
{
    public class UserResponseModel
    {
        public Guid UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? Role { get; set; }

        public string? Phone { get; set; }

        public string? ProfileUrl { get; set; }

        public string? Metadata { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? LastLogin { get; set; }
    }
}
