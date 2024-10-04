using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Models;

public partial class PerfumeEdition
{
    public Guid PerfumeEditionId { get; set; }

    public Guid PerfumeId { get; set; }

    public string Name { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string ScentIntensity { get; set; } = null!;

    public string Availability { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual Perfume Perfume { get; set; } = null!;

    public virtual ICollection<PerfumeCart> PerfumeCarts { get; set; } = new List<PerfumeCart>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
