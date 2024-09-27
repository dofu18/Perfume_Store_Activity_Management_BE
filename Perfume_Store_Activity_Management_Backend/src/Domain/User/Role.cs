namespace Perfume_Store_Activity_Management_Backend.src.Domain.User;

public class Role
{
    public Guid RoleId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<User> Users { get; set; }
}
