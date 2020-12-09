namespace Common.ErrorHandling
{
    public class ValidationException : ApiException
    {
        public ValidationException(string message = null, params object[] args)
            : base(400, message, args)
        {
        }

        public ValidationException(ValidationException[] details, string message = "Default")
            : base(400, message)
        {
            Details = details ?? new ApiException[0];
        }
    }
}
