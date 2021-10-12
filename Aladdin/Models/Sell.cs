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
        [Key]
        public int SellID{ get; set; }
        public int ProductID { get; set; }
        public int Quantity{ get; set; }
        public int CustomerID { get; set; }
        [DataType(DataType.Date)]
        public DateTime PDate { get; set; }
    }
}
