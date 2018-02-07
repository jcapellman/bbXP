using System.Collections.Generic;
using System.Threading.Tasks;
using bbxp.lib.Common;
using bbxp.lib.Settings;
using bbxp.lib.Transports.Posts;

namespace bbxp.lib.Handlers {
    public class PostTagHandler : BaseHandler {
        public PostTagHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "PostTag";

        public async Task<ReturnSet<List<PostResponseItem>>> GetPostsFromTag(string tag) => await GetAsync<ReturnSet<List<PostResponseItem>>>($"tag={tag}");
    }
}