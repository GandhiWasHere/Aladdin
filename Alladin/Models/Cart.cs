using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alladin.Models
{
    public class Cart
    {


        public int CartID{ get; set; }
        public int CustomerID { get; set; }

        public int CartTotal{ get; set; }

        public ICollection<Product> CartProducts{ get; set; }
    }
}
