using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Model;

public partial class Cart
{
    public Guid CartId { get; set; }

    public Guid UserId { get; set; }

    public Guid PerfumeId { get; set; }

    public int Quantity { get; set; }

    public DateTime? DateAdded { get; set; }

    public virtual PerfumeProduct Perfume { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
