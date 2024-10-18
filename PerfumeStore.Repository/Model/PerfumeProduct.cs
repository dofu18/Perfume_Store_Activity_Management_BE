using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Model;

public partial class PerfumeProduct
{
    public Guid PerfumeId { get; set; }

    public string Name { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Scent { get; set; } = null!;

    public string? Gender { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int ViewCount { get; set; }

    public string Origin { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public int Volume { get; set; }

    public int Discount { get; set; }

    public string TopNote { get; set; } = null!;

    public string MiddleNote { get; set; } = null!;

    public string BaseNote { get; set; } = null!;

    public DateTime? DateAdded { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<PerfumeCharacteristic> PerfumeCharacteristics { get; set; } = new List<PerfumeCharacteristic>();

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
