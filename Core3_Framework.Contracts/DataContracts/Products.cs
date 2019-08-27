using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core3_Framework.Contracts.DataContracts
{
    public class Products
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public int QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        //[NotMapped]
        public Categories Category { get; set; }

        public virtual Categories Categories { get; set; }
    }
}
