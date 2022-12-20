using bbxp.lib.Configuration;

using bbxp.web.mvc.Models.Base;

namespace bbxp.web.mvc.Models
{
    public class IndexModel : BaseViewModel
    {
        public IndexModel(AppConfiguration config) : base(config)
        {
            Posts = new List<PostViewModel>();
        }

        public List<PostViewModel> Posts { get; set; }
    }
}