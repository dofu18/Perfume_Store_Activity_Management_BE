using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class CreateOrderRequest
    {
        public decimal? Amount { get; set; } = null!;
        public required string Currency { get; set; }
        public Guid? OrderId { get; set; } = Guid.Empty;
    }

    public class CaptureOrderRequest
    {
        public string? OrderId { get; set; }
        public Guid UserId { get; set; }
    }
}
