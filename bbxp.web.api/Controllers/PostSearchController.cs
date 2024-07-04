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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1862:Use the 'StringComparison' method overloads to perform case-insensitive string comparisons", Justification = "Not supported in Cloud SQL")]
        public async Task<List<Posts>> SearchPostsAsync([FromRoute] string searchQuery)
        {
            searchQuery = searchQuery.ToLower();

            return await dbContext.Posts.AsNoTracking().Where(a => a.Title.ToLower().Contains(searchQuery)).ToListAsync();
        }
     
    }
}