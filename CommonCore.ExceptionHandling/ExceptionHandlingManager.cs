using CommonCore.ExceptionHandling.Interfaces;
using CommonCore.ExceptionHandling.Types;
using CommonCore.Interfaces;
using CommonCore.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonCore.ExceptionHandling
{
    public class ExceptionHandlingManager : IExceptionHandlingManager
    {
        private readonly IConfiguration configuration;
        public readonly ILogHelper logger;

        private readonly List<ExceptionType> exceptions;
        public ExceptionHandlingManager(IConfiguration _configuration, ILogHelper _logger)
        {
            configuration = _configuration;
            logger = _logger;
            exceptions = new List<ExceptionType>();
            configuration.GetSection("ExceptionTypes").Bind(exceptions);

        }

        public CustomException HandleException(Exception ex)
        {
            var DebugInfoInJson = true;
            var customException = new CustomException();
            string debugInfo = "";

            var exception = exceptions.Where(x => x._Type == ex.GetType().ToString()).FirstOrDefault();
            if (exception != null)
            {
                customException.Message = (string.IsNullOrEmpty(exception._ReplaceMessage) ? ex.Message : exception._ReplaceMessage);
                if (exception._HideDebugInfo == "false")
                {
                    if (DebugInfoInJson)
                    {
                        debugInfo = new JsonExceptionFormatter().GetMessage(ex);
                    }
                    else
                    {
                        debugInfo = new ExceptionFormatter().GetMessage(ex);
                    }
                    customException.DebugInfo = debugInfo;
                }
                customException.StackTrace = ex.StackTrace;
                customException.ErrorCode = (string.IsNullOrEmpty(exception._ErrorCode) ? ex.HResult.ToString() : exception._ErrorCode);
                customException.Display = (string.IsNullOrEmpty(exception._HandlingAction) && exception._HandlingAction == "true" ? true : false);

                if (string.IsNullOrEmpty(exception._LogAfterHandling) && exception._LogAfterHandling == "true")
                {
                    Type type = this.GetType().UnderlyingSystemType;
                    String className = type.Name;
                    String nameSpace = type.Namespace;

                    logger.Log(nameSpace + "." + className + "|" + debugInfo, CommonCore.Types.Enums.LogType.Error);
                }

            }
            else
            {
                if (DebugInfoInJson)
                {
                    customException.DebugInfo = new JsonExceptionFormatter().GetMessage(ex);
                }
                else
                {
                    customException.DebugInfo = new ExceptionFormatter().GetMessage(ex);
                }
                customException.Message = ex.Message;
                customException.StackTrace = ex.StackTrace;
                customException.ErrorCode = ex.HResult.ToString();
                customException.Display = true;
            }

            return customException;
        }
    }
}
