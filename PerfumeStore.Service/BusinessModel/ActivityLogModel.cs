using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class ActivityLogModel
    {
        public Guid ActivityId { get; set; }

        public Guid PerfumeId { get; set; }

        public string? Action { get; set; }

        public DateTime? Date { get; set; }

        public string? Notes { get; set; }
    }
}
