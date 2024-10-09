using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }

        public PagedResult(List<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }
    }
}
