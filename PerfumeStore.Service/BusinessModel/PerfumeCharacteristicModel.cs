using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class PerfumeCharacteristicModel
    {
        public Guid CharacteristicId { get; set; }

        public Guid PerfumeId { get; set; }

        public string? AttributeName { get; set; }

        public string? AttributeValue { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
