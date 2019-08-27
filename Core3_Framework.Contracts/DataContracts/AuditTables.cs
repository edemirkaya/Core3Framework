using System;

namespace Core3_Framework.Contracts.DataContracts
{
    public class AuditTables
    {
        public string TableName { get; set; }

        public Type GetEntityType()
        {
            return Type.GetType("Core3_Framework.Contracts.DataContracts." + TableName);
        }
    }
}
