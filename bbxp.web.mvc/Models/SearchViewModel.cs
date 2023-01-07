using bbxp.lib.Configuration;
using bbxp.web.mvc.Models.Base;

namespace bbxp.web.mvc.Models
{
    public class SearchViewModel : BaseViewModel
    {
        public SearchViewModel(AppConfiguration config) : base(config)
        {
            Posts = new List<PostViewModel>();

            SearchQuery = string.Empty;
        }

        public string SearchQuery { get; set; }

        public List<PostViewModel> Posts { get; set; }
    }
}