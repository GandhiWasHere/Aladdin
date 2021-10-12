using Aladdin.Data;
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

        public class DataValuePair
        {
            public string Data { get; set; }
            public string Value { get; set; }
        }

        public class SearchResult
        {
            public string Query { get; set; }
            public List<DataValuePair> Suggestions { get; set; }
        }
        public async Task<IActionResult> statproduct()
        {
            DataValuePair e1 = new DataValuePair { Data = "ss", Value = "EE" };
            var te = new List<DataValuePair>();
            te.Add(e1);
            SearchResult mySearchResult = new SearchResult { Query="eeee", Suggestions =te };
            var sse = Json(mySearchResult);


            //var all_sells = from e in _context.Sell select e;
            var all_sells = (from e in _context.Sell join p in _context.Product on e.ProductID equals p.ProductID select new { p.ProductName, e.Quantity }).ToList();
            var ProductsSells2 = all_sells.GroupBy(x => x.ProductName).ToList();
            List<string> ProductNames1 = new List<string>();
            List<int> counter1 = new List<int>();
            //ProductsSells2 =ProductsSells2.ToList();
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
            //var ProductNames = ProductNames1.ToArray();
            //var counter = counter1.ToArray();

            ViewData["ProductNames"] = ProductNames1;
            ViewData["counter"] = counter1;


            //var ProductsSells = all_sells.GroupBy(x => x.ProductName, (key, item) => new ProductsSells { productName = key, Count = item.Count() });
            //var ProductsSells = all_sells.GroupBy(x => x.ProductID, (key, item) => new ProductsSells { productID = key ,Count =item.Count() });

            /*List<int> ProductIDs = new List<int>();
            List<int> counter = new List<int>();
            foreach (var item in ProductsSells)
            {
                ProductIDs.Add(item.productID);
                counter.Add(item.Count);
            }
            var ProductIDs1 = ProductIDs.ToArray();
            var counter2 = counter.ToArray();
            ViewData["ProductIDs1"] = ProductIDs1;
            ViewData["counter2"] = counter2;
*/
            return View();
            //return View(await ProductsSells.ToListAsync());
        }



    }
}