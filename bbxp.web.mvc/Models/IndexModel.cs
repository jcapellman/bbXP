using bbxp.lib.Configuration;

using bbxp.web.mvc.Models.Base;

namespace bbxp.web.mvc.Models
{
    public class IndexModel : BaseViewModel
    {
        public IndexModel(AppConfiguration config) : base(config)
        {
            Posts = new List<PostViewModel>();

            Categories = new List<string>();
        }

        public List<PostViewModel> Posts { get; set; }

        public List<string> Categories { get; set; }
    }
}