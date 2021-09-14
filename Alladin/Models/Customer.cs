﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alladin.Models
{
    public class Customer
    {

        public int CustomerID{ get; set; }
        [DisplayName("Username")]
        [Required(ErrorMessage ="You Must enter username")]
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerRole { get; set; }

        public string CustomerPhoneNumber { get; set; }
        public int CartID { get; set; }

        // I added this
        public string ErrorMessage { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "You Must enter password")]
        [DataType(DataType.Password)]
        public string CustomerPassword { get; set; }
    }
}
