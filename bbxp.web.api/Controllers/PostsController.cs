using bbxp.lib.Common;
using bbxp.lib.Database;
using bbxp.lib.Database.Tables;

using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController(BbxpContext dbContext, IMemoryCache memoryCache, ILogger<PostsController> logger) : BaseController(memoryCache)
    {

        [HttpGet]
        [Route("{category}/{postCount}/")]
        public async Task<List<Posts>> GetPostsAsync([FromRoute] string category, [FromRoute] int postCount)
        {
            try
            {
                var cacheResult = GetFromCache<List<Posts>>(category);

                if (cacheResult is not null)
                {
                    return cacheResult;
                }

                List<Posts> dbResult = [];

                dbResult = category switch
                {
                    LibConstants.POST_REQUEST_DEFAULT_CATEGORY =>
                         await dbContext.Posts.Where(a => a.Active).OrderByDescending(a => a.PostDate).Take(postCount).ToListAsync(),
                    _ =>
                        await dbContext.Posts.Where(a => a.Active && a.Category == category).OrderByDescending(a => a.PostDate).ToListAsync(),
                };

                return AddToCache(category, dbResult);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to Get Posts due to {ex}", ex);

                throw;
            }
        }

        [HttpGet]
        [Route("{url}")]
        public ActionResult<Posts?> GetSinglePostAsync([FromRoute] string url)
        {
            try
            {
                var cacheResult = GetFromCache<Posts>(url);

                if (cacheResult is not null)
                {
                    return cacheResult;
                }

                var dbResult = dbContext.Posts.FirstOrDefault(a => a.Active && a.URL == url);

                if (dbResult is null)
                {
                    logger.LogDebug("Post ({url}) was not found", url);

                    return NotFound($"Post ({url}) was not found");
                }

                return AddToCache(url, dbResult);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to Get Post due to {ex}", ex);

                throw;
            }
        }
    }
}