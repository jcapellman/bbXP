using bbxp.lib.Database;
using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class CacheController(IMemoryCache memoryCache) : BaseController(memoryCache)
    {
        [Authorize]
        [HttpGet]
        public void FlushCache()
        {
            ClearCache();
        }
    }
}