using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Aladdin.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        public int SupplierID { get; set; }
        public int ProductRating { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImage { get; set; }
        [DisplayName("S")]
        public int ProductQuantityS{ get; set; }
        [DisplayName("M")]
        public int ProductQuantityM { get; set; }
        [DisplayName("L")]
        public int ProductQuantityL { get; set; }
    }
}
