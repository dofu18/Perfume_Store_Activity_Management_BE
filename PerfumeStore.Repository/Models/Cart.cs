using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Models;

public partial class Cart
{
    public Guid CartId { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<PerfumeCart> PerfumeCarts { get; set; } = new List<PerfumeCart>();
}
