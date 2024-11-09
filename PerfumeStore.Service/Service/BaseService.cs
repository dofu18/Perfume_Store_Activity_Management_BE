using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PerfumeStore.Repository.Model;
using PerfumeStore.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.Service
{
    public abstract class BaseService
    {
        protected readonly PerfumeStoreContext _dbContext;
        protected readonly IHttpContextAccessor _httpCtx;


        public BaseService(IHttpContextAccessor httpCtx, PerfumeStoreContext dbContext)
        {
            _dbContext = dbContext;
            _httpCtx = httpCtx;
        }

        protected JwtPayload? ExtractPayload()
        {
            var ctx = _httpCtx.HttpContext;
            if (ctx == null) return null;
            var payload = ctx.Items["payload"] as JwtPayload;
            return payload;
        }
    }
}
