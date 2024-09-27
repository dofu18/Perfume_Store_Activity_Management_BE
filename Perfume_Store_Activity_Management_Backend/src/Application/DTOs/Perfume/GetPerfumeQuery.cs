namespace Perfume_Store_Activity_Management_Backend.src.Application.DTOs.Perfume;

using Perfume_Store_Activity_Management_Backend.src.Application.DTOs.Pagination;

public class GetPerfumeQuery : PaginationReq
{
    public string? SearchKeyword { get; set; } = null!;
}
