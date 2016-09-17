using System.Linq;

using bbxp.MVC.Containers;
using bbxp.MVC.Entities;
using bbxp.MVC.Models;

namespace bbxp.MVC.Managers {
    public class StatsManager : BaseManager {
        public StatsManager(ManagerContainer container) : base(container) { }

        public StatsViewModel GetStatsOverview() {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var topRequests = eFactory.DGT_MostFrequentedPages.ToList();

                var requestsHeader = eFactory.DGT_MostFrequentedPagesHeader.FirstOrDefault();

                return new StatsViewModel {
                    TopRequests = topRequests,
                    NumberRequests = requestsHeader.RequestCount,
                    CurrentAsOf = requestsHeader.CurrentAsOf
                };
            }
        }
    }
}