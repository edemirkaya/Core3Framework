using CommonCore.ExceptionHandling.Types;
using System;

namespace CommonCore.ExceptionHandling.Interfaces
{
    public interface IExceptionHandlingManager
    {
        CustomException HandleException(Exception ex);
    }
}
