using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Model;

public partial class ActivityLog
{
    public Guid ActivityId { get; set; }

    public Guid UserId { get; set; }

    public Guid PerfumeId { get; set; }

    public string? Action { get; set; }

    public DateTime? Date { get; set; }

    public string? Notes { get; set; }

    public virtual PerfumeProduct Perfume { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
