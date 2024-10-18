using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Model;

public partial class Notification
{
    public Guid NotificationId { get; set; }

    public Guid UserId { get; set; }

    public string? Message { get; set; }

    public DateTime? DateSent { get; set; }

    public Guid SentBy { get; set; }

    public virtual User SentByNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
