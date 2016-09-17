using bbxp.MVC.Containers;

namespace bbxp.MVC.Managers {
    public class BaseManager {
        protected ManagerContainer mContainer;

        public BaseManager(ManagerContainer container) {
            mContainer = container;
        }
    }
}