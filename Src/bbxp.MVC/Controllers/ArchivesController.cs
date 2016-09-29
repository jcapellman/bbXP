using System;
using System.Threading.Tasks;

using bbxp.PCL.Handlers;
using bbxp.PCL.Settings;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace bbxp.MVC.Controllers {
    public class ArchivesController : BaseController {
        public ArchivesController(IOptions<GlobalSettings> globalSettings) : base(globalSettings.Value) { }

        public async Task<IActionResult> Index() {
            var archiveList = await new PostArchiveHandler(MANAGER_CONTAINER.GSetings).GetArchiveList();

            if (archiveList.HasError) {
                throw new Exception(archiveList.ExceptionMessage);
            }

            return View(archiveList.ReturnValue);
        }

        [Route("/archives/{year}/{month}")]
        public async Task<PartialViewResult> ArchivePosts(int year, string month) {
            var monthInt = Convert.ToDateTime(month + " 01, 1900").Month;

            var posts = await new PostArchiveHandler(MANAGER_CONTAINER.GSetings).GetPostsFromMonth(year, monthInt);

            if (posts.HasError) {
                throw new Exception(posts.ExceptionMessage);
            }

            ViewData["Title"] = $"{month} {year}";

            return PartialView("_ArchivedPosts", posts.ReturnValue);
        }
    }
}