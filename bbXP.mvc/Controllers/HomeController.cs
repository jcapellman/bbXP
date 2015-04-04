using Microsoft.AspNet.Mvc;

namespace bbXP.mvc.Controllers {
    public class HomeController : BaseController {		
        public IActionResult Index() {
			ViewBag.Title = GlobalVariables.SITE_NAME;

			/*
			var model = new Models.HomeModel(baseModel);

			var postManager = new PostManager();
			var currentDate = DateTime.Now;

			model.Posts = postManager.GetPosts(new DateTime(currentDate.Year, currentDate.Month, 1), new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month)));

			if (model.Posts.Count == 0) {
				currentDate = currentDate.AddMonths(-1);

				model.Posts = postManager.GetPosts(new DateTime(currentDate.Year, currentDate.Month, 1), new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month)));
			}


			ViewBag.Model = model;
			ViewBag.SinglePost = false;

			return View(model);
			*/

			return View();
			
		}
    }
}