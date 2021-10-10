using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Aladdin.Models
{
    public class Supplier
    {
        public int SupplierID{ get; set; }
        public String SupplierName{ get; set; }
        [DisplayName("SupplierPhoneNumber")]
        public int SupplierPhonNumber{ get; set; }
        public ICollection<Product> SupplierProducts { get; set; }
        //public ICollection<Product> ProductID { get; set; }

    }
}
