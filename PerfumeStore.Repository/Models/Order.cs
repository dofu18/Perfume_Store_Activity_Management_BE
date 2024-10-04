using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid CartId { get; set; }

    public string Status { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
