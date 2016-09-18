using System.Threading.Tasks;
using bbxp.PCL.Common;
using bbxp.PCL.Settings;
using bbxp.PCL.Transports.PageStats;

namespace bbxp.PCL.Handlers {
    public class PageStatsHandler : BaseHandler {
        public PageStatsHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "PageStats";

        public async Task<ReturnSet<PageStatsResponseItem>> GetPageStats() => await GetAsync<ReturnSet<PageStatsResponseItem>>();
    }
}