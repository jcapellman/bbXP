using System;
using System.Linq;

using bbxp.lib.Common;
using bbxp.lib.Containers;
using bbxp.lib.Transports.PageStats;

using bbxp.web.DAL;

namespace bbxp.web.Managers {
    public class PageStatsManager : BaseManager {
        public PageStatsManager(ManagerContainer container) : base(container) { }

        public ReturnSet<PageStatsResponseItem> GetStatsOverview()
        {
            var (isFound, cachedResult) = GetCachedItem<PageStatsResponseItem>("PageStats");

            if (isFound)
            {
                return new ReturnSet<PageStatsResponseItem>(cachedResult);
            }

            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var topRequests = eFactory.DGT_MostFrequentedPages.ToList();

                var requestsHeader = eFactory.DGT_MostFrequentedPagesHeader.FirstOrDefault();

                if (requestsHeader == null)
                {
                    return new ReturnSet<PageStatsResponseItem>(new Exception("No header found"));
                }

                var request = new ReturnSet<PageStatsResponseItem>(new PageStatsResponseItem {
                    TopRequests = topRequests.Select(a => new PageRequestStatsResponseItem {
                        Count = a.Count,
                        ID = a.ID
                    }).ToList(),
                    NumberRequests = requestsHeader.RequestCount,
                    CurrentAsOf = requestsHeader.CurrentAsOf
                });

                AddCachedItem("PageStats", request);

                return request;
            }
        }
    }
}