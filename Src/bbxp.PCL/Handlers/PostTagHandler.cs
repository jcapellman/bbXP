using System.Collections.Generic;
using System.Threading.Tasks;
using bbxp.PCL.Common;
using bbxp.PCL.Settings;
using bbxp.PCL.Transports.Posts;

namespace bbxp.PCL.Handlers {
    public class PostTagHandler : BaseHandler {
        public PostTagHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "PostTag";

        public async Task<ReturnSet<List<PostResponseItem>>> GetPostsFromTag(string tag) => await GetAsync<ReturnSet<List<PostResponseItem>>>($"tag={tag}");
    }
}