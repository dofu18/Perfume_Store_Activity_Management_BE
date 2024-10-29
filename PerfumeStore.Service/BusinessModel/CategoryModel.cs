using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class CategoryModel
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;
    }
}
