using bbxp.lib.Database;
using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class CacheController(BbxpContext dbContext, IMemoryCache memoryCache) : BaseController(dbContext, memoryCache)
    {
        [HttpGet]
        public void FlushCache()
        {
            ClearCache();
        }
    }
}