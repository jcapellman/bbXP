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

                // Use compiled queries for better performance (eliminates query translation overhead)
                List<Posts> dbResult = category switch
                {
                    LibConstants.POST_REQUEST_DEFAULT_CATEGORY =>
                        await BbxpContext.GetActivePostsAsync(dbContext, postCount).ToListAsync(),
                    _ =>
                        await BbxpContext.GetPostsByCategoryAsync(dbContext, category).ToListAsync(),
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
        public async Task<ActionResult<Posts?>> GetSinglePostAsync([FromRoute] string url)
        {
            try
            {
                var cacheResult = GetFromCache<Posts>(url);

                if (cacheResult is not null)
                {
                    return cacheResult;
                }

                // Use compiled query for single post lookup
                var dbResult = await BbxpContext.GetPostByUrlAsync(dbContext, url);

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

        [HttpGet]
        [Route("{category}/{postCount}/{offset}")]
        public async Task<List<Posts>> GetPostsPagedAsync([FromRoute] string category, [FromRoute] int postCount, [FromRoute] int offset)
        {
            try
            {
                var cacheKey = $"{category}_{postCount}_{offset}";
                var cacheResult = GetFromCache<List<Posts>>(cacheKey);

                if (cacheResult is not null)
                {
                    return cacheResult;
                }

                // Use compiled queries for paged results
                List<Posts> dbResult = category switch
                {
                    LibConstants.POST_REQUEST_DEFAULT_CATEGORY =>
                        await BbxpContext.GetActivePostsPagedAsync(dbContext, offset, postCount).ToListAsync(),
                    _ =>
                        await BbxpContext.GetPostsByCategoryPagedAsync(dbContext, category, offset, postCount).ToListAsync(),
                };

                return AddToCache(cacheKey, dbResult);
            }
            catch (Exception ex)
            {
                logger.LogError("Failed to Get Paged Posts due to {ex}", ex);

                throw;
            }
        }
    }
}