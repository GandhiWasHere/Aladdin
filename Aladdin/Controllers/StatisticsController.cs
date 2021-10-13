using Aladdin.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aladdin.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly AladdinContext _context;

        public StatisticsController(AladdinContext context)
        {
            _context = context;
        }

        public class CustomerAddressView
        {
            public string Address;
            public int Count;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var customer_list = from c in _context.Customer select c;
            var shimi = customer_list.GroupBy(x => x.CustomerAddress, (key, item) => new CustomerAddressView
            {
                Address = key,
                Count = item.Count()
            });

            return View(await shimi.ToListAsync());
        }





        public class ProductsSells
        {
            //public int productID;
            public string productName;
            public int Count;
        }

        [Authorize]
        public async Task<IActionResult> ByProduct()
        {


            var all_sells = (from e in _context.Sell join p in _context.Product on e.ProductID equals p.ProductID select new { p.ProductName, e.Quantity }).ToList();
            var ProductsSells2 = all_sells.GroupBy(x => x.ProductName).ToList();
            List<string> ProductNames1 = new List<string>();
            List<int> counter1 = new List<int>();
    
            foreach (var item in ProductsSells2)
            {
                ProductNames1.Add(item.Key);
                int temp = 0;
                foreach (var item2 in item)
                {
                    temp = temp + item2.Quantity;
                }
                counter1.Add(temp);
            }

            ViewData["ProductNames"] = ProductNames1;
            ViewData["counter"] = counter1;


            return View();
            
        }









    }
}