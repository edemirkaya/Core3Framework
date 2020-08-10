using System;
using System.Collections.Generic;

namespace Core3_Framework.Contracts.DataContracts
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
