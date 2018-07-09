using bbxp.lib.Transports.Posts;

namespace bbxp.web.Models
{
    public class PostModel
    {
        public PostResponseItem Post { get; set; }

        public bool IsSinglePost { get; set; }
    }
}