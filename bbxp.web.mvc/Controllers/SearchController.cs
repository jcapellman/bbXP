using bbxp.lib.Configuration;

using bbxp.web.mvc.Controllers.Base;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.mvc.Controllers
{
    public class SearchController : BaseController
    {
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger, IOptions<AppConfiguration> appConfiguration) : base(appConfiguration)
        {
            _logger = logger;
        }

        public ActionResult Index() => View();
    }
}