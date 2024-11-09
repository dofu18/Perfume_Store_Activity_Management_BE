using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class TransactionModel
    {
        public Guid TransactionId { get; set; }

        public Guid OrderId { get; set; }

        public string? PaymentMethod { get; set; }

        public string? PaymentStatus { get; set; }

        public DateTime? TransactionDate { get; set; }
    }
}
