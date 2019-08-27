using System;
using System.Collections.Generic;
using System.Text;

namespace Core3_Framework.Contracts.DataContracts
{
    public class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string SeoURL { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
