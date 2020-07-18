using Newtonsoft.Json;
using System;

namespace bbxp.lib.Common
{
    public class ReturnSet<T>
    {
        [JsonProperty("ReturnValue")]
        public T ReturnValue;

        [JsonProperty("ExceptionMessage")]
        public string ExceptionMessage;

        public bool HasError => !string.IsNullOrEmpty(ExceptionMessage);

        public ReturnSet(T obj)
        {
            ReturnValue = obj;
        }

        public ReturnSet(string exception = null)
        {
            ExceptionMessage = exception;
        }

        public ReturnSet(Exception ex)
        {
            ExceptionMessage = ex.ToString();
        }

        public ReturnSet() { }
    }
}