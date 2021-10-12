using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aladdin.Data;
using Aladdin.Models;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;

namespace Aladdin.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AladdinContext _context;

        public CustomersController(AladdinContext context)
        {
            _context = context;
        }

        // GET: Customers
        [Authorize]
        public async Task<IActionResult> Index( )
        {
           return View(await _context.Customer.ToListAsync());
         
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> Create([Bind("CustomerID,CustomerName,CustomerPassword,CustomerAddress,CustomerImage,CustomerEmail,CustomerPhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var max_id = _context.Customer.Any() ? _context.Customer.Max(m => m.CustomerID) + 1 : 1;
                Cart cart = new();
                //cart.CartID = max_id;
                cart.CustomerID = max_id;
                customer.CartID = max_id;
                _context.Add(customer);
                _context.Add(cart);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                //return View("Succefully");
                //return View("~/Views/admin/Succefully.cshtml");
                return RedirectToAction("Successfuly", "Admin");
            }
            //return View("unSuccessfuly", "Admin");
            return RedirectToAction("Unsuccessfuly", "Admin");
            //return View(customer);
        }




        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }


        [HttpPost]
        public async Task<IActionResult> Edit([Bind("CustomerID,CustomerName,CustomerAddress,CustomerEmail,CustomerRole,CustomerPhoneNumber,CartID,CustomerPassword,CustomerImage")] Customer customer)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Successfuly", "Admin");
            }
            return RedirectToAction("unSuccessfuly", "Admin");
            //return View(customer);
        }


        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Successfuly", "Admin");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerID == id);
        }
    }
}
