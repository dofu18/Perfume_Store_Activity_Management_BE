using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Model;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public Guid OrderId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? TransactionDate { get; set; }

    public virtual Order Order { get; set; } = null!;
}
