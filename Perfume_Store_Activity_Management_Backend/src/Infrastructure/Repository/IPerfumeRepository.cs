using Perfume_Store_Activity_Management_Backend.src.Application.DTOs.Perfume;
using Perfume_Store_Activity_Management_Backend.src.Domain.Perfume;

namespace Perfume_Store_Activity_Management_Backend.src.Infrastructure.Repository
{

    public class PerfumeList
    {
        public IEnumerable<Perfume> Perfumes { get; set; } = new List<Perfume>();
        public int TotalCount { get; set; }
    }

    public interface IPerfumeRepository
    {
        PerfumeList GetPerfumeList(GetPerfumeQuery req);
    }
}
