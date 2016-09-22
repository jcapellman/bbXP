using System;
using System.Net.Http;
using System.Threading.Tasks;

using bbxp.PCL.Handlers;
using bbxp.PCL.Transports.Content;

namespace bbxp.UWP.ViewModels {
    public class ContentViewModel : BaseViewModel {
        private ContentResponseItem _contentResponseItem;

        public ContentResponseItem Content {
            get { return _contentResponseItem; }
            set { _contentResponseItem = value; OnPropertyChanged(); }
        }

        private async Task<string> GetCSS(string url) => await new HttpClient().GetStringAsync(url);
        
        public async Task<bool> LoadData(string urlArg) {
            var contentHandler = new ContentHandler(gSettings);

            var content = await contentHandler.GetContent(urlArg);

            if (content.HasError) {
                throw new Exception(content.ExceptionMessage);
            }

            Content = content.ReturnValue;

            var bootStrap = await GetCSS("http://www.jarredcapellman.com/lib/bootstrap/dist/css/bootstrap.min.css");

            var siteCSS = await GetCSS("http://www.jarredcapellman.com/css/site.min.css?v=28d7-GIU5eioseEXBdSMHG0se-bH1hLCc0xSNO58Rek");

            Content.Body = $"<head><meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\" /><style type='text/css'>{bootStrap}{siteCSS}</style></head><body><div class=\"container body-content\"><div id=\"PostContainer\"><div id=\"ContentContainer\">{Content.Body}</div></div></div></body>";

            return true;
        }
    }
}