using System;
using System.Collections.Generic;

namespace Core3_Framework.Contracts.DataContracts
{
    public partial class OrderSubtotals
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
