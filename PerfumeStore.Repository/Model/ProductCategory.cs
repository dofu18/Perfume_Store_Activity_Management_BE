using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Model;

public partial class ProductCategory
{
    public Guid ProductCategoryId { get; set; }

    public Guid PerfumeId { get; set; }

    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual PerfumeProduct Perfume { get; set; } = null!;
}
