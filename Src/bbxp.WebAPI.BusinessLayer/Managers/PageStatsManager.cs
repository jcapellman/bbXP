using System.Linq;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Containers;
using bbxp.CommonLibrary.Transports.PageStats;

using bbxp.WebAPI.DataLayer.Entities;

namespace bbxp.WebAPI.BusinessLayer.Managers {
    public class PageStatsManager : BaseManager {
        public PageStatsManager(ManagerContainer container) : base(container) { }

        public ReturnSet<PageStatsResponseItem> GetStatsOverview() {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var topRequests = eFactory.DGT_MostFrequentedPages.ToList();

                var requestsHeader = eFactory.DGT_MostFrequentedPagesHeader.FirstOrDefault();

                return new ReturnSet<PageStatsResponseItem>(new PageStatsResponseItem {
                    TopRequests = topRequests.Select(a => new PageRequestStatsResponseItem {
                        Count = a.Count,
                        ID = a.ID
                    }).ToList(),
                    NumberRequests = requestsHeader.RequestCount,
                    CurrentAsOf = requestsHeader.CurrentAsOf
                });
            }
        }
    }
}