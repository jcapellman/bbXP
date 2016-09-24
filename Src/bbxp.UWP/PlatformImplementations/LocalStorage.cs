using System;
using System.Threading.Tasks;

using bbxp.PCL.Common;
using bbxp.PCL.PlatformAbstractions;

namespace bbxp.UWP.PlatformImplementations {
    public class LocalStorage : ILocalStorage {
        public Task<T> ReadFile<T>(string fileName) {
            throw new NotImplementedException();
        }

        public Task<ReturnSet<bool>> WriteFile<T>(string fileName, T objectValue) {
            throw new NotImplementedException();
        }
    }
}