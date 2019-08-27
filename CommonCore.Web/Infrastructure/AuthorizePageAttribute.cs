using System;

namespace CommonCore.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class AuthorizePageAttribute : Attribute
    {
        public int[] PageIds { get; private set; }

        public AuthorizePageAttribute(params int[] pageIds)
        {
            PageIds = pageIds;
        }
    }
}
