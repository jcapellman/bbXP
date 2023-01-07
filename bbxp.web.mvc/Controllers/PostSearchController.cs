using bbxp.lib.Configuration;
using bbxp.lib.HttpHandlers;
using bbxp.web.mvc.Controllers.Base;
using bbxp.web.mvc.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.web.mvc.Controllers
{
    [Route("[controller]/[action]")]
    public class PostSearchController : BaseController
    {
        private readonly ILogger<PostSearchController> _logger;

        public PostSearchController(ILogger<PostSearchController> logger, IOptions<AppConfiguration> appConfiguration) : base(appConfiguration)
        {
            _logger = logger;
        }

        [HttpGet("{searchQuery}/")]
        public async Task<IActionResult> Index(string? searchQuery = null)
        {
            var model = new SearchViewModel(_appConfiguration);

            if (searchQuery == null)
            {
                ViewData["Title"] = "Search";

                model.SearchQuery = string.Empty;

                return View(model);
            }

            var postHttpHandler = new PostHttpHandler(_appConfiguration.APIUrl);

            var posts = await postHttpHandler.SearchPostsAsync(searchQuery);

            model.Posts = posts.Select(a => new PostViewModel(_appConfiguration) { Post = a }).ToList();
            model.SearchQuery = searchQuery;

            ViewData["Title"] = searchQuery;

            return View(model);
        }
    }
}