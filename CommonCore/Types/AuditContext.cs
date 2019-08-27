using System;
using System.Collections.Generic;

namespace CommonCore.Types
{
    public class AuditContext
    {
        public Guid CorrelationId { get; set; }
        public string Username { get; set; }

        public int UserId { get; set; }
        public Dictionary<string, string> AdditionalData { get; set; }
    }
}
