using System.Collections.Generic;

using bbxp.PCL.Transports.Posts;

namespace bbxp.PCL.Transports.PostArchive {
    public class PostArchiveListingWrapper {
        public string DateString { get; set; }

        public string RelativeURL { get; set; }

        public int Count { get; set; }

        public List<PostResponseItem> Posts { get; set; }

        public PostResponseItem SelectedPost { get; set; }
    }
}