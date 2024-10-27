using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class JwtPayload
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        public Guid SessionId { get; set; }
        public string Status { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
