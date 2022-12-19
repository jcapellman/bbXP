using bbxp.lib.Configuration;
using bbxp.lib.HttpHandlers;
using bbxp.web.mvc.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace bbxp.web.mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppConfiguration _appConfiguration;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IOptions<AppConfiguration> appConfiguration)
        {
            _logger = logger;
            _appConfiguration = appConfiguration.Value;
        }

        public async Task<IActionResult> Index()
        {
            var postHttpHandler = new PostHttpHandler(_appConfiguration.APIUrl);

            var model = new IndexModel();

            model.Posts = await postHttpHandler.GetPostsAsync();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}