using System.Linq;
using bbxp.PCL.Common;
using bbxp.PCL.Containers;
using bbxp.PCL.Transports.PageStats;
using bbxp.WebAPI.DataLayer.Entities;

namespace bbxp.WebAPI.BusinessLayer.Managers {
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