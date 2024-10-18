using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Model;

public partial class Feedback
{
    public Guid FeedbackId { get; set; }

    public Guid PerfumeId { get; set; }

    public Guid UserId { get; set; }

    public int? Rating { get; set; }

    public string? Comments { get; set; }

    public DateTime? Date { get; set; }

    public virtual PerfumeProduct Perfume { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
