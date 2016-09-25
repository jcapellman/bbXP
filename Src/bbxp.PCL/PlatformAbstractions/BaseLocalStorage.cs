﻿using bbxp.PCL.Common;

using System.Threading.Tasks;

using Newtonsoft.Json;

namespace bbxp.PCL.PlatformAbstractions {
    public abstract class BaseLocalStorage {
        public abstract Task<ReturnSet<bool>> WriteFile<T>(string fileName, T objectValue);

        public abstract Task<dynamic> ReadFile<T>(string fileName);

        protected string ConvertToJSON<T>(T objectValue) => JsonConvert.SerializeObject(objectValue);

        protected T ConvertJSONtoT<T>(string json) => (T) JsonConvert.DeserializeObject(json);
    }
}