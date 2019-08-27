using System;

namespace CommonCore.Interfaces
{
    public interface IAuditLogEntity
    {
        int Id { get; set; }
        Guid? RelationId { get; set; }
        string TableName { get; set; }
        string KeyValues { get; set; }
        string ProcessType { get; set; }
        string ProcessOwner { get; set; }
        DateTime ProcessTime { get; set; }
        string OldValues { get; set; }
        string NewValues { get; set; }
        string AdditionalData { get; set; }
    }
}
