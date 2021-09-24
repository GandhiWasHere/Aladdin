using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aladdin.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ProductSize{ get; set; }
        public string ProductColor { get; set; }
        public ICollection<Supplier> ProductSuppliers{ get; set; }
        public ICollection<Cart> ProductCarts { get; set; }
        public int ProductRating { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImage { get; set; }
    }
}
