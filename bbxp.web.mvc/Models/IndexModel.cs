using bbxp.lib.Configuration;
using bbxp.lib.Database.Tables;
using bbxp.web.mvc.Models.Base;

namespace bbxp.web.mvc.Models
{
    public class IndexModel : BaseViewModel
    {
        public IndexModel(AppConfiguration config) : base(config)
        {
            Posts = new List<Posts>();
        }

        public List<Posts> Posts { get; set; }
    }
}