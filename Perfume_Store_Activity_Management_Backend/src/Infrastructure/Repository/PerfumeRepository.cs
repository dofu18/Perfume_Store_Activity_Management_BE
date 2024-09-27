using Microsoft.CodeAnalysis.CSharp.Syntax;
using Perfume_Store_Activity_Management_Backend.src.Application.DTOs.Perfume;
using Perfume_Store_Activity_Management_Backend.src.Infrastructure.Database;

namespace Perfume_Store_Activity_Management_Backend.src.Infrastructure.Repository
{
    public class PerfumeRepository : IPerfumeRepository
    {
        private readonly PerfumeStoreDbContext _dbContext;

        public PerfumeRepository(PerfumeStoreDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public PerfumeList GetPerfumeList(GetPerfumeQuery req)
        {
            string searchKeyword = req.SearchKeyword ?? "";

            var query = _dbContext.Perfumes
                .Where(c => c.Bartender.Contains(searchKeyword) || (c.FlavorGroup.Contains(searchKeyword)))
                .AsQueryable();

            var totalCount = query.Count();

            var perfumes = query
                .Skip(req.Page * req.PageSize)
                .Take(req.PageSize)
                .ToList();
            return new PerfumeList { Perfumes = perfumes, TotalCount = totalCount };
        }
    }
}
