using System;

namespace CommonCore.Web.Interfaces
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class UnauthorizedAccessAttribute : Attribute
    {
        public bool MustAuthenticated { get; private set; }

        public UnauthorizedAccessAttribute(bool mustAuthenticated)
        {
            MustAuthenticated = mustAuthenticated;
        }
    }
}
