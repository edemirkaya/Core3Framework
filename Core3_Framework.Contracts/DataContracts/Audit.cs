using System;

namespace Core3_Framework.Contracts.DataContracts
{
    public class Audits
    {
        public int Id { get; set; }
        public Guid? RelationId { get; set; }
        public string TableName { get; set; }
        public string KeyValues { get; set; }
        public string ProcessType { get; set; }
        public string ProcessOwner { get; set; }
        public DateTime ProcessTime { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AdditionalValue { get; set; }
    }
}
