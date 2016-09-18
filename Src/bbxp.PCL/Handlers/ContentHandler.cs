using System.Threading.Tasks;
using bbxp.PCL.Common;
using bbxp.PCL.Settings;
using bbxp.PCL.Transports.Content;

namespace bbxp.PCL.Handlers {
    public class ContentHandler : BaseHandler {
        public ContentHandler(GlobalSettings globalSettings) : base(globalSettings) { }

        protected override string BaseControllerName() => "Content";

        public async Task<ReturnSet<ContentResponseItem>> GetContent(string urlArg) => await GetAsync<ReturnSet<ContentResponseItem>>($"urlArg={urlArg}");        
    }
}