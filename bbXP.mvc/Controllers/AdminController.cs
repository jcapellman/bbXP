using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using bbXP.businessLayer.Managers;
using bbXP.dataLayer.Models;

namespace bbXP.mvc.Controllers {
    public class AdminController : BaseController {
        // GET: /<controller>/
        public IActionResult Index() {
            return View();
        }

	    public ActionResult Edit(int id) {
		    try {
			    var pManager = new PostManager();

			    var post = pManager.GetPostForAdmin(id);

			    return View("Edit", post);
		    } catch (Exception ex) {
			    return RedirectToRoute("Shared/Error");
		    }
	    }

	    public async Task<ActionResult> Save(Post post) {
			var pManager = new PostManager();

		    var result = await pManager.UpdatePost(post);

		    return View("Index");
	    }
    }
}