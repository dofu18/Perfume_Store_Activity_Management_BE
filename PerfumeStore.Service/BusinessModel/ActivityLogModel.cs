using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfumeStore.Repository.Enum;

namespace PerfumeStore.Service.BusinessModel
{
    public class ActivityLogModel
    {
        public Guid ActivityId { get; set; }

        public Guid PerfumeId { get; set; }

        public ActionEnum? Action { get; set; }

        public DateTime? Date { get; set; }

        public string? Notes { get; set; }
    }


    public class UpdateActivityLogModel
    {
        public ActionEnum? Action { get; set; }

        public string? Notes { get; set; }
    }
}
