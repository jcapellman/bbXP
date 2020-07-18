using bbxp.lib.DAL;
using bbxp.lib.Managers;
using bbxp.lib.Settings;
using bbxp.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Linq;

namespace bbxp.web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(BbxpDbContext dbContext, IMemoryCache cache, IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value, cache, dbContext) { }

        public IActionResult Index()
        {
            var result = new PostManager(ManagerContainer).GetHomeListing();

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View(result.ReturnValue.Select(a => new PostModel { IsSinglePost = false, Post = a }).ToList());
        }

        [Route("tag/{urlSafeTagName}")]
        public IActionResult TagResult(string urlSafeTagName)
        {
            var result = new PostManager(ManagerContainer).GetPostsFromTag(urlSafeTagName);

            return result.HasError ? RedirectToError(result.ExceptionMessage) : View("Index", result.ReturnValue.Select(a => new PostModel { IsSinglePost = false, Post = a }).ToList());
        }

        [Route("{year}/{month}/{day}/{postURL}")]
        // ReSharper disable once InconsistentNaming
        public IActionResult LegacyRoute(int year, int month, int day, string postURL) => SinglePost(postURL);

        [Route("{postURL}")]
        // ReSharper disable once InconsistentNaming
        public IActionResult SinglePost(string postURL)
        {
            var post = new PostManager(ManagerContainer).GetSinglePost(postURL);

            if (post.HasError)
            {
                return RedirectPermanent(postURL);
            }

            ViewData["Title"] = post.ReturnValue.Title;

            return View("_PostPartial", new PostModel
            {
                IsSinglePost = true,
                Post = post.ReturnValue
            });
        }
    }
}