using System.Threading.Tasks;

using bbxp.lib.Settings;

using bbxp.web.Managers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.Controllers {
    public class SearchController : BaseController {
        public SearchController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public IActionResult Index() => View();

        [Route("search/{query}")]
        public async Task<PartialViewResult> Search(string query)
            => PartialView("_SearchResults", await new PostManager(MANAGER_CONTAINER).SearchPostsAsync(query));
    }
}