using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Models;

public partial class Role
{
    public Guid RoleId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
