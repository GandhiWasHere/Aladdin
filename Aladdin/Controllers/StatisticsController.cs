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
    }
}