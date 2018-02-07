using System.Threading.Tasks;
using bbxp.lib.Common;
using bbxp.lib.Settings;
using bbxp.lib.Transports.PageStats;

namespace bbxp.lib.Handlers {
    public class PageStatsHandler : BaseHandler {
        public PageStatsHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "PageStats";

        public async Task<ReturnSet<PageStatsResponseItem>> GetPageStats() => await GetAsync<ReturnSet<PageStatsResponseItem>>();
    }
}