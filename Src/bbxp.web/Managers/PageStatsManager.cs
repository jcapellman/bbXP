using System.Linq;

using bbxp.lib.Common;
using bbxp.lib.Containers;
using bbxp.lib.Transports.PageStats;

namespace bbxp.web.Managers {
    public class PageStatsManager : BaseManager {
        public PageStatsManager(ManagerContainer container) : base(container) { }

        public ReturnSet<PageStatsResponseItem> GetStatsOverview() {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var topRequests = eFactory.DGT_MostFrequentedPages.ToList();

                var requestsHeader = eFactory.DGT_MostFrequentedPagesHeader.FirstOrDefault();

                var request = new ReturnSet<PageStatsResponseItem>(new PageStatsResponseItem {
                    TopRequests = topRequests.Select(a => new PageRequestStatsResponseItem {
                        Count = a.Count,
                        ID = a.ID
                    }).ToList(),
                    NumberRequests = requestsHeader.RequestCount,
                    CurrentAsOf = requestsHeader.CurrentAsOf
                });

                rFactory.WriteJSON("PageStats", request);

                return request;
            }
        }
    }
}