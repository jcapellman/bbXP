using bbxp.lib.Database;
using bbxp.web.api.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/postcategories")]
    public class PostCategoriesController(BbxpContext dbContext, IMemoryCache memoryCache, ILogger<PostCategoriesController> logger) : BaseController(dbContext, memoryCache)
    {
        private readonly ILogger<PostCategoriesController> _logger = logger;

        [HttpGet]
        public IOrderedQueryable<string> GetPostCategoriesAsync()
        {
            try
            {
                return GetCategoriesAsync();
            }
            catch (Exception ex) {
                _logger.LogError("Failed to obtain Categories {ex}", ex);

                throw;
            }
        }
    }
}