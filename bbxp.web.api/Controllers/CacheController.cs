using bbxp.lib.Database;
using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class CacheController : BaseController
    {
        private readonly ILogger<CacheController> _logger;

        public CacheController(bbxpDbContext dbContext, IMemoryCache memoryCache, ILogger<CacheController> logger) : base(dbContext, memoryCache)
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