using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class GgAuthInfo
    {
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileUrl { get; set; }
    }

    public class GgAuthResp
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? RedirectLink { get; set; }
    }
}
