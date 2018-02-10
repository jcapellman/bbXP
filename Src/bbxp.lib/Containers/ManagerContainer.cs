using bbxp.lib.DAL;
using bbxp.lib.Settings;

using Microsoft.Extensions.Caching.Memory;

namespace bbxp.lib.Containers {
    public class ManagerContainer {

        public GlobalSettings GSetings { get; set; }

        public IMemoryCache Cache { get; set; }

        public BbxpDbContext DBContext { get; set; }
    }
}