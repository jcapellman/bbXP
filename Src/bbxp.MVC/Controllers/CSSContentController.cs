using Microsoft.AspNetCore.Mvc;

namespace bbxp.MVC.Controllers {
    public class CSSContentController : Controller {
        [ResponseCache(Duration = 3600)]
        public string Index() {
            var bootStrap = System.IO.File.ReadAllText("wwwroot/lib/bootstrap/dist/css/bootstrap.min.css");

            var siteCSS = System.IO.File.ReadAllText("wwwroot/css/site.css");

            return bootStrap + siteCSS;
        }
    }
}