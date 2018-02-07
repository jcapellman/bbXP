using System.Collections.Generic;
using System.Linq;

using bbxp.PCL.Common;
using bbxp.PCL.Containers;
using bbxp.PCL.Enums;
using bbxp.PCL.Transports.PostArchive;

using bbxp.WebAPI.DataLayer.Entities;

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

                rFactory.WriteJSON(MainCacheKeys.PostArchive, response);

                return response;
            }
        }
    }
}