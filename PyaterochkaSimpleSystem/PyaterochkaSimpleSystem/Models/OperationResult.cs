using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyaterochkaSimpleSystem.Models
{
    internal class OperationResult<T>
    {
        public T Result { get; private set; }
        public Exception? Error { get; private set; }

        private OperationResult(T result)
        {
            Result = result;
            Error = null;
        }

        private OperationResult(Exception error)
        {
            Error = error;
        }

        public static OperationResult<T> Success(T result)
        {
            return new OperationResult<T>(result);
        }

        public static OperationResult<T> Failure(Exception error)
        {
            return new OperationResult<T>(error);
        }
    }
}
