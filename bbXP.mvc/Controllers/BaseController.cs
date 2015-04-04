using bbXP.pcl.Transports.Global;

using Microsoft.AspNet.Mvc;

namespace bbXP.mvc.Controllers {
    public class BaseController : Controller {
		[Activate]
		public GlobalVars GlobalVariables { get; set; }
	}
}