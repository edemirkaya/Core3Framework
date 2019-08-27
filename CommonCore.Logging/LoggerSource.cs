using CommonCore.Interfaces;
using CommonCore.Types.Enums;
using NLog;
using System;

namespace CommonCore.Logging
{
    public class LoggerSource : ILogHelper
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public LoggerSource()
        {
        }

        public bool Log(string message, LogType logType)
        {
            if (!String.IsNullOrEmpty(message))
            {
                switch (logType)
                {
                    case LogType.Info:
                        logger.Info(message);
                        break;
                    case LogType.Warning:
                        logger.Warn(message);
                        break;
                    case LogType.Error:
                        logger.Error(message);
                        break;
                }
                return true;
            }
            return false;
        }

        public bool LogException(Exception exception)
        {
            //Wİndows dışı ortamlar için EventLog a yazmamak gerekiyor
            return false;
        }
    }
}
