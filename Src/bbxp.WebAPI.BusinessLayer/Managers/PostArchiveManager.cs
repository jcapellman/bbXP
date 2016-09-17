using System.Collections.Generic;
using System.Linq;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Containers;
using bbxp.CommonLibrary.Transports.PostArchive;

using bbxp.WebAPI.DataLayer.Entities;

namespace bbxp.WebAPI.BusinessLayer.Managers {
    public class PostArchiveManager : BaseManager {
        public PostArchiveManager(ManagerContainer container) : base(container) { }

        public ReturnSet<List<PostArchiveListingResponseItem>> GetArchives() {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var result = eFactory.DGT_Archives.OrderByDescending(a => a.PostDate).ToList();

                return new ReturnSet<List<PostArchiveListingResponseItem>>(result.Select(a => new PostArchiveListingResponseItem {
                    Count = a.Count,
                    RelativeURL = a.RelativeURL,
                    DateString = a.DateString
                }).ToList());
            }
        }
    }
}