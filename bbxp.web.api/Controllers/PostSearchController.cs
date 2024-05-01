using bbxp.lib.Database.Tables;
using bbxp.web.api.Controllers.Base;

using LimDB.lib;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/postsearch")]
    public class PostSearchController(LimDbContext<Posts> dbContext, IMemoryCache memoryCache, ILogger<PostSearchController> logger) : BaseController(dbContext, memoryCache)
    {
        [HttpGet]
        [Route("{searchQuery}")]
        public IEnumerable<Posts> SearchPosts([FromRoute] string searchQuery) => GetPostsFromSearchAsync(searchQuery);
    }
}