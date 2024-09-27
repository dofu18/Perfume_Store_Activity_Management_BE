using Perfume_Store_Activity_Management_Backend.src.Application.DTOs.Pagination;

namespace Perfume_Store_Activity_Management_Backend.src.Application.DTOs.Perfume;

public class PerfumeResp : PaginationResp
{
    public List<PerfumeDTO> Data { get; set; } = null!;
}
