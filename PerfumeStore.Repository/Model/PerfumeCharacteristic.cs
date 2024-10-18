using System;
using System.Collections.Generic;

namespace PerfumeStore.Repository.Model;

public partial class PerfumeCharacteristic
{
    public Guid CharacteristicId { get; set; }

    public Guid PerfumeId { get; set; }

    public string? AttributeName { get; set; }

    public string? AttributeValue { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual PerfumeProduct Perfume { get; set; } = null!;
}
