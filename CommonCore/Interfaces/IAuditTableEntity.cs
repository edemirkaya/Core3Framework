using System;

namespace CommonCore.Interfaces
{
    public interface IAuditTableEntity
    {
        string TableName { get; set; }
        Type GetEntityType();
    }
}
