namespace Common.ErrorHandling
{
    public class ErrorMessage
    {
        public int Error { get; set; }
        public string Message { get; set; }
        public ErrorMessage[] Details { get; set; }
    }
}
