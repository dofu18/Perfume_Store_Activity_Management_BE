using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class UserModel
    {
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime LastLoginAt { get; set; }
    }
}
