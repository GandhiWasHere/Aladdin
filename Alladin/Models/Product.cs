using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alladin.Models
{
    public class Product
    {

        public int ProductID { get; set; }
        public string ProductName { get; set; }

        public int ProductSize{ get; set; }

        public string ProductColor { get; set; }
        // ze kielu ha tavlat kishur shel pruct-supplier
        
        public ICollection<Supplier> ProductSuppliers{ get; set; }
        // le eze agalot ha pduct meshuyah
        public ICollection<Cart> ProductCarts { get; set; }

        public int ProductRating { get; set; }
        public int ProductPrice { get; set; }


    }
}
