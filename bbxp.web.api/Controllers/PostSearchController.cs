using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/postsearch")]
    public class PostSearchController(BbxpContext dbContext, IMemoryCache memoryCache) : BaseController(memoryCache)
    {
        [HttpGet]
        [Route("{searchQuery}")]
        public async Task<List<Posts>> SearchPostsAsync([FromRoute] string searchQuery) => await dbContext.Posts.Where(a => a.Title.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase)).ToListAsync();
     
    }
}