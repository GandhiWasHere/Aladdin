using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aladdin.Models
{ 
    public class ProductInCart
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        //public int SupplierID { get; set; }
        public int ProductRating { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int ProductQuantityS { get; set; }
        public int ProductQuantityM { get; set; }
        public int ProductQuantityL { get; set; }
        public ICollection<Cart> ProductCarts { get; set; }
    }
}


