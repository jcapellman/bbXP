using System.Threading.Tasks;
using bbxp.lib.Handlers;
using bbxp.lib.Settings;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Options;

namespace bbxp.MVC.Controllers {
    public class SearchController : BaseController {
        public SearchController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public IActionResult Index() => View();

        [Route("search/{query}")]
        public async Task<PartialViewResult> Search(string query)
            => PartialView("_SearchResults", await new SearchHandler(MANAGER_CONTAINER.GSetings).SearchPosts(query));
    }
}