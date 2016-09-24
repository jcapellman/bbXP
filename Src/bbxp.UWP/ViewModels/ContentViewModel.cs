using System;
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
        
        public async Task<bool> LoadData(string urlArg) {
            var contentHandler = new ContentHandler(GSettings);

            var content = await contentHandler.GetContent(urlArg);

            if (content.HasError) {
                throw new Exception(content.ExceptionMessage);
            }

            Content = content.ReturnValue;
            
            return true;
        }
    }
}