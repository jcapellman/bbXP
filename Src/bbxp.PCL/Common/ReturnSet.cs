using System;
using Newtonsoft.Json;

namespace bbxp.PCL.Common {    
    public class ReturnSet<T> {
        [JsonProperty("ReturnValue")]
        public T ReturnValue;

        [JsonProperty("ExceptionMessage")]
        public string ExceptionMessage;

        public bool HasError => !string.IsNullOrEmpty(ExceptionMessage);

        public ReturnSet(T obj) {
            ReturnValue = obj;
        }

        public ReturnSet(string exception = null) {
            ExceptionMessage = exception;
        }

        public ReturnSet(Exception ex) {
            ExceptionMessage = ex.ToString();
        }
        
        public ReturnSet() { }
    }
}