using bbxp.lib.Database.Tables;
using bbxp.web.api.Controllers.Base;
using LimDB.lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class CacheController : BaseController
    {
        private readonly ILogger<CacheController> _logger;

        public CacheController(LimDbContext<Posts> dbContext, IMemoryCache memoryCache, ILogger<CacheController> logger) : base(dbContext, memoryCache)
        {
            _logger = logger;
        }

        [HttpGet]
        public void FlushCache()
        {
            ClearCache();
        }
    }
}