using bbxp.lib.HttpHandlers;
using bbxp.web.mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace bbxp.web.mvc.Controllers
{
    public class HomeController : Controller
    {
        private const string REST_SERVICE_BASE_URL = "https://localhost:7026/api/";


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var postHttpHandler = new PostHttpHandler(REST_SERVICE_BASE_URL);

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