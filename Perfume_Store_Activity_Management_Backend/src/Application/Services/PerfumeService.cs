using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Perfume_Store_Activity_Management_Backend.src.Application.DTOs.Perfume;
using Perfume_Store_Activity_Management_Backend.src.Application.DTOs.Shared.Type;
using Perfume_Store_Activity_Management_Backend.src.Infrastructure.Database;
using Perfume_Store_Activity_Management_Backend.src.Infrastructure.Repository;

namespace Perfume_Store_Activity_Management_Backend.src.Application.Services
{
    public interface IPerfumeService
    {
        public Task<IActionResult> GetAllPerfumes(GetPerfumeQuery req);
    }

    public class PerfumeService : IPerfumeService
    {

        private readonly IPerfumeRepository _perfumeRepository;

        private readonly IMapper _mapper;

        public PerfumeService(IMapper mapper, PerfumeStoreDbContext dbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _perfumeRepository = new PerfumeRepository(dbContext);
        }

        public async Task<IActionResult> GetAllPerfumes(GetPerfumeQuery req)
        {
            var perfumes = _perfumeRepository.GetPerfumeList(req);
            var perfumeDto = _mapper.Map<List<PerfumeDTO>>(perfumes.Perfumes);
            var resp = new PerfumeResp
            {
                Data = perfumeDto,
                Total = perfumes.TotalCount,
                Page = req.Page,
                PageSize = req.PageSize
            };

            return SuccessResp.Ok(resp);
        }
    }
}
