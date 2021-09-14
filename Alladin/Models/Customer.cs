using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alladin.Models
{
    public class Customer
    {

        public int CustomerID{ get; set; }
        
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        public int CustomerPhoneNumber { get; set; }
        public int CartID { get; set; }
    }
}
