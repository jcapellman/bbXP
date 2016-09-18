using System;
using System.Runtime.Serialization;

namespace bbxp.CommonLibrary.Common {
    [DataContract]
    public class ReturnSet<T> {
        [DataMember]
        public T ReturnValue;

        [DataMember]
        public string ExceptionMessage;

        public bool HasError => !string.IsNullOrEmpty(ExceptionMessage);

        public ReturnSet(T obj) {
            ReturnValue = obj;
        }

        public ReturnSet(Exception ex) {
            ExceptionMessage = ex.ToString();
        }

        public ReturnSet(string exception) {
            ExceptionMessage = exception;
        }

        public ReturnSet() { }
    }
}