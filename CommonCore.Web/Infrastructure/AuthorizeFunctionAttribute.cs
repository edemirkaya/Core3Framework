using System;

namespace CommonCore.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class AuthorizeFunctionAttribute : Attribute
    {
        public int[] FunctionIds { get; private set; }

        public AuthorizeFunctionAttribute(params int[] functionIds)
        {
            FunctionIds = functionIds;
        }
    }
}
