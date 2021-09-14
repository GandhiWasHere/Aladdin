using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alladin.Models
{
    public class Supplier
    {
        public int SupplierID{ get; set; }
        public String SupplierName{ get; set; }
        public int SupplierPhonNumber{ get; set; }

        public ICollection<Product> SupplierProducts { get; set; }

    }
}
