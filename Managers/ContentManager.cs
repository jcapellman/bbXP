using System.Linq;

using bbxp.MVC.Containers;
using bbxp.MVC.Entities;
using bbxp.MVC.Models;

namespace bbxp.MVC.Managers {
    public class ContentManager : BaseManager {
        public ContentManager(ManagerContainer container) : base(container) { }

        public ContentViewModel GetContent(string urlSafeName) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var content = eFactory.Content.FirstOrDefault(a => a.URLSafename == urlSafeName && a.Active);

                return new ContentViewModel { Body = content.Body, Title = content.Title };
            }
        }
    }
}