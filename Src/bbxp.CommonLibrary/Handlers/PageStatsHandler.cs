using System.Threading.Tasks;

using bbxp.CommonLibrary.Common;
using bbxp.CommonLibrary.Settings;
using bbxp.CommonLibrary.Transports.PageStats;

namespace bbxp.CommonLibrary.Handlers {
    public class PageStatsHandler : BaseHandler {
        public PageStatsHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "PageStats";

        public async Task<ReturnSet<PageStatsResponseItem>> GetPageStats() => await GetAsync<ReturnSet<PageStatsResponseItem>>();
    }
}