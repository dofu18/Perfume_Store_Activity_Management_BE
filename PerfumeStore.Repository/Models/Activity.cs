using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Models;

public partial class Activity
{
    public Guid ActivityId { get; set; }

    public Guid PerfumeId { get; set; }

    public Guid CustomerId { get; set; }

    public DateTime LastUsed { get; set; }

    public string Comment { get; set; } = null!;

    public int Rating { get; set; }

    public virtual User Customer { get; set; } = null!;

    public virtual PerfumeEdition Perfume { get; set; } = null!;
}
