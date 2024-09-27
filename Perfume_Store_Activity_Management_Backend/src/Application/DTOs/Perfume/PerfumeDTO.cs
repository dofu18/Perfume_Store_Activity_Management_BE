namespace Perfume_Store_Activity_Management_Backend.src.Application.DTOs.Perfume;

using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Perfume_Store_Activity_Management_Backend.src.Domain.Perfume;

public class PerfumeDTO
{
    public Guid PerfumeId { get; set; }
    public int ViewCount { get; set; }
    public string Origin { get; set; }
    public int ReleaseYear { get; set; }
    public string Concentration { get; set; }
    public string Bartender { get; set; }
    public string FlavorGroup { get; set; }
    public int Capacity { get; set; }
    public decimal Price { get; set; }
    public int Discount { get; set; }
    public string TopNote { get; set; }
    public string MiddleNote { get; set; }
    public string BaseNote { get; set; }
}

public class PerfumeProfile : Profile
{
    public PerfumeProfile()
    {
        CreateMap<Perfume,PerfumeDTO>();
    }
}
