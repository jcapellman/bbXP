using Microsoft.AspNetCore.Mvc;

namespace bbxp.MVC.Controllers {
    public class CSSContentController : Controller {
        public string Index() {
            var bootStrap = System.IO.File.ReadAllText("wwwroot/lib/bootstrap/dist/css/bootstrap.min.css");

            var siteCSS = System.IO.File.ReadAllText("wwwroot/css/site.css");

            return bootStrap + siteCSS;
        }
    }
}