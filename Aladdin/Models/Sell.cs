using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aladdin.Models
{
    public class Sell
    {
        public int ProductID { get; set; }
        public int Quantity{ get; set; }
    }
}
