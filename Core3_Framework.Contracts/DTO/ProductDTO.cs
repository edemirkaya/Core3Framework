using System;
using System.Collections.Generic;
using System.Text;

namespace Core3_Framework.Contracts.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public int QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
