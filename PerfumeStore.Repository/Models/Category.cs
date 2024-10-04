using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Models;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<PerfumeEdition> Perfumes { get; set; } = new List<PerfumeEdition>();
}
