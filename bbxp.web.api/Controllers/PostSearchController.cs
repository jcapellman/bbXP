using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/postsearch")]
    public class PostSearchController : BaseController
    {
        private readonly ILogger<PostSearchController> _logger;

        public PostSearchController(bbxpDbContext dbContext, IMemoryCache memoryCache, ILogger<PostSearchController> logger) : base(dbContext, memoryCache)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{searchQuery}")]
        public async Task<ActionResult<List<Posts>>> SearchPosts(string searchQuery)
        {
            try
            {
                return await GetPostsFromSearchAsync(searchQuery);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to search posts due to an exception.  Query was {searchQuery}", searchQuery);

                throw;
            }
        }
    }
}