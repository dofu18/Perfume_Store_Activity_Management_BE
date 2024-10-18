using PerfumeStore.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class CartModel
    {
        public Guid CartId { get; set; }

        public Guid UserId { get; set; }

        public Guid PerfumeId { get; set; }

        public int Quantity { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}
