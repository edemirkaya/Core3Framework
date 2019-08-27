using System;

namespace CommonCore.Interfaces
{
    public interface ILogHelper
    {
        bool Log(string message, CommonCore.Types.Enums.LogType logType);
        bool LogException(Exception exception);
    }
}
