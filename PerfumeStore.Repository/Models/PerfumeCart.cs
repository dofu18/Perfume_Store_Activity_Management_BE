using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Models;

public partial class PerfumeCart
{
    public Guid PerfumeId { get; set; }

    public Guid CartId { get; set; }

    public decimal TotalPrice { get; set; }

    public int Quantity { get; set; }

    public int Discount { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual PerfumeEdition Perfume { get; set; } = null!;
}
