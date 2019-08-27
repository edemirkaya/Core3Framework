using CommonCore.ExceptionHandling.Interfaces;

namespace CommonCore.ExceptionHandling.Types
{
    public class CustomException// : Exception
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }


        public string DebugInfo { get; set; }

        public string ErrorCode { get; set; }

        public ExceptionType ExceptionType { get; set; }

        public bool Display { get; set; }

        public CustomException()
        {
        }

        //public CustomException(string message, Exception innerException) : base(message, innerException)
        //{
        //}
    }
}
