using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel.CustomResponse
{
    public class BaseResp
    {
        public int Code { get; set; }
        public string Message { get; set; } = null!;
    }
}
