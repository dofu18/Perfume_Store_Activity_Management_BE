using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string ProfileUrl { get; set; } = null!;

    public string Metadata { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime LastLoginAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
