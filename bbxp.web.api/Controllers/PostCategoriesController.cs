using bbxp.lib.Database;
using bbxp.web.api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace bbxp.web.api.Controllers
{
    [ApiController]
    [Route("api/postcategories")]
    public class PostCategoriesController : BaseController
    {
        private readonly ILogger<PostCategoriesController> _logger;

        public PostCategoriesController(bbxpDbContext dbContext, IMemoryCache memoryCache, ILogger<PostCategoriesController> logger) : base(dbContext, memoryCache) {
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<string>> GetPostCategoriesAsync()
        {
            try
            {
                return await GetCategoriesAsync();
            }
            catch (Exception ex) {
                _logger.LogError("Failed to obtain Categories {ex}", ex);

                throw;
            }
        }
    }
}