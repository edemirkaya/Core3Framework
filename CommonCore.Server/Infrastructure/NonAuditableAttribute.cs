using System;

namespace CommonCore.Server.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NonAuditableAttribute : Attribute
    {
    }
}
