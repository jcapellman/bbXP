using bbxp.MVC.Managers;
using bbxp.MVC.Settings;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Options;

namespace bbxp.MVC.Controllers {
    public class SearchController : BaseController {
        public SearchController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public IActionResult Index() => View();

        [Route("search/{query}")]
        public PartialViewResult Search(string query) => PartialView("_SearchResults", new PostManager(MANAGER_CONTAINER).SearchPosts(query));
    }
}