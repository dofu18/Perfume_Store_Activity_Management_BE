using System.Data;

namespace Perfume_Store_Activity_Management_Backend.src.Domain.User;

public class User
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string ProfileUrl { get; set; }
    public string Metadata { get; set; }
    public string Status { get; set; }
    public DateTime LastLoginAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Role Role { get; set; }
}
