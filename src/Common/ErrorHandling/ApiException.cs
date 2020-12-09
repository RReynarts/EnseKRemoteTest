using System;

namespace Common.ErrorHandling
{
    public class ApiException : ApplicationException
    {
        public ApiException(int error, string message, params object[] args)
            : base(message)
        {
            Error = error;
            Arguments = args;
        }
        public int Error { get; }
        public object[] Arguments { get; }
        
        public ApiException[] Details { get; protected set; } = new ApiException[0];
    }
}
