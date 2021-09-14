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

        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }

        public string CustomerRole { get; set; }

        public string CustomerPhoneNumber { get; set; }
        public int CartID { get; set; }
    }
}
