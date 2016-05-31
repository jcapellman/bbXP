using System.Collections.Generic;
using System.Linq;

using bbxp.MVC.Containers;
using bbxp.MVC.Entities;
using bbxp.MVC.Models;

namespace bbxp.MVC.Managers {
    public class ArchivesManager : BaseManager {
        public ArchivesManager(ManagerContainer container) : base(container) { }

        public List<ArchiveListViewModel> GetArchives() {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var result = eFactory.DGT_Archives.OrderByDescending(a => a.PostDate).ToList();

                return result.Select(a => new ArchiveListViewModel {
                    Count = a.Count,
                    RelativeURL = a.RelativeURL,
                    DateString = a.DateString
                }).ToList();
            }
        }
    }
}