using bbxp.lib.Database;
using bbxp.lib.Database.Tables;
using bbxp.web.api.Controllers.Base;
using LimDB.lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/postcategories")]
    public class PostCategoriesController : BaseController
    {
        private readonly ILogger<PostCategoriesController> _logger;

        public PostCategoriesController(LimDbContext<Posts> dbContext, IMemoryCache memoryCache, ILogger<PostCategoriesController> logger) : base(dbContext, memoryCache) {
            _logger = logger;
        }

        [HttpGet]
        public IOrderedEnumerable<string> GetPostCategoriesAsync()
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