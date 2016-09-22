using System.Linq;

using bbxp.PCL.Common;
using bbxp.PCL.Containers;
using bbxp.PCL.Transports.Content;

using bbxp.WebAPI.DataLayer.Entities;

namespace bbxp.WebAPI.BusinessLayer.Managers {
    public class ContentManager : BaseManager {
        public ContentManager(ManagerContainer container) : base(container) { }

        public ReturnSet<ContentResponseItem> GetContent(string urlSafeName) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var content = eFactory.Content.FirstOrDefault(a => a.URLSafename == urlSafeName && a.Active);

                if (content == null) {
                    return new ReturnSet<ContentResponseItem>($"{urlSafeName} cannot be found");
                }

                var response = new ReturnSet<ContentResponseItem>(new ContentResponseItem { Body = content.Body, Title = content.Title });

                rFactory.WriteJSON(content.URLSafename, response);

                return response;
            }
        }
    }
}