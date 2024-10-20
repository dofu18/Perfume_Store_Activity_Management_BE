﻿using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Model;

public partial class OrderItem
{
    public Guid OrderItemId { get; set; }

    public Guid OrderId { get; set; }

    public Guid PerfumeId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual PerfumeProduct Perfume { get; set; } = null!;
}
