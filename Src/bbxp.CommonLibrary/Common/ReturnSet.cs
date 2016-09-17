using System;

namespace bbxp.CommonLibrary.Common {
    public class ReturnSet<T> {
        public T ReturnValue;

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