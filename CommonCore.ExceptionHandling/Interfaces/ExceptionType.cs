namespace CommonCore.ExceptionHandling.Interfaces
{
    public class ExceptionType
    {
        public string _Type { get; set; }
        public string _HandlingAction { get; set; }
        public string _ReplaceMessage { get; set; }
        public string _ErrorCode { get; set; }
        public string _LogAfterHandling { get; set; }
        public string _HideDebugInfo { get; set; }
    }
}
