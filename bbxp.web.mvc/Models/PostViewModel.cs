using bbxp.lib.Configuration;
using bbxp.lib.Database.Tables;

using bbxp.web.mvc.Models.Base;

namespace bbxp.web.mvc.Models
{
    public class PostViewModel : BaseViewModel
    {
        public PostViewModel(AppConfiguration config) : base(config)
        {
        }

        public required Posts Post { get; set; }
    }
}