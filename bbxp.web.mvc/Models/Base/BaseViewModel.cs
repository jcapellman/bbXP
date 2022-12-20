using bbxp.lib.Configuration;

namespace bbxp.web.mvc.Models.Base
{
    public class BaseViewModel
    {
        public AppConfiguration Config { get; private set; }

        public BaseViewModel(AppConfiguration config) {
            Config = config;
        }
    }
}