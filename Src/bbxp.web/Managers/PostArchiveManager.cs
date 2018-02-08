using System.Collections.Generic;
using System.Linq;

using bbxp.lib.Common;
using bbxp.lib.Containers;
using bbxp.lib.Transports.PostArchive;

using bbxp.web.DAL;

namespace bbxp.web.Managers {
    public class PostArchiveManager : BaseManager {
        public PostArchiveManager(ManagerContainer container) : base(container) { }

        public ReturnSet<List<PostArchiveListingResponseItem>> GetArchives() {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var result = eFactory.DGT_Archives.OrderByDescending(a => a.PostDate).ToList();

                var response = new ReturnSet<List<PostArchiveListingResponseItem>>(result.Select(a => new PostArchiveListingResponseItem {
                    Count = a.Count,
                    RelativeURL = a.RelativeURL,
                    DateString = a.DateString
                }).ToList());
                
                return response;
            }
        }
    }
}