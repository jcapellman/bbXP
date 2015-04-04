using Microsoft.AspNet.Mvc;

namespace bbXP.mvc.Controllers {
    public class BaseController : Controller {
		[Activate]
		public bbXP.businessLayer.Global.GlobalVars GlobalVariables { get; set; }
	}
}