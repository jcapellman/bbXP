using bbxp.lib.DAL;
using bbxp.lib.Managers;
using bbxp.lib.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class SearchController : BaseController {
        public SearchController(BbxpDbContext dbContext, IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache, dbContext) { }

        public IActionResult Index() => View();

        [Route("search/{query}")]
        public PartialViewResult Search(string query)
        {
            var result = new PostManager(ManagerContainer).SearchPosts(query);

            return result.HasError ? PartialView("Error", result.ExceptionMessage) : PartialView("_SearchResults", result.ReturnValue);
        }            
    }
}