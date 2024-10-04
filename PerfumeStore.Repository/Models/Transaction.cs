using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Models;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public string Message { get; set; } = null!;

    public string Resource { get; set; } = null!;

    public string Meta { get; set; } = null!;

    public Guid CreatedBy { get; set; }

    public Guid UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
