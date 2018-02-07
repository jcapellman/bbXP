using bbxp.lib.Containers;

namespace bbxp.web.Managers {
    public class BaseManager {
        protected ManagerContainer mContainer;
        
        public BaseManager(ManagerContainer container) {
            mContainer = container;
        }
    }
}