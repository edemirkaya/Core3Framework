using CommonCore.Types.Enums;

namespace CommonCore.Server.Services
{
    public class ServiceResult<T>
    {
        public ServiceResult()
        {
            ServiceResultType = EnumServiceResultType.Success;
        }
        public ServiceResult(T result)
        {
            ServiceResultType = EnumServiceResultType.Success;
            Result = result;
        }
        public T Result { get; set; }
        public string Message { get; set; }
        public EnumServiceResultType ServiceResultType { get; set; }
        public bool IsError { get; set; }

    }
}
