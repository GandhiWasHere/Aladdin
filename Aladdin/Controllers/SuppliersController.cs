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
using Microsoft.AspNetCore.Authorization;

namespace Aladdin.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly AladdinContext _context;

        public SuppliersController(AladdinContext context)
        {
            _context = context;
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
                return View(await _context.Supplier.ToListAsync());
        }


        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier
                .FirstOrDefaultAsync(m => m.SupplierID == id);
            if (supplier == null)
            {
                return NotFound();
            }


        var SuppProducts = (from s in _context.Supplier join p in _context.Product on s.SupplierID equals p.SupplierID select new { p.ProductID, p.ProductName, p.ProductPrice, s.SupplierID }).ToList();

            List<int> productsids = new List<int>();
            List<int> productsprices = new List<int>();
            List<string> productsnames = new List<string>();

            for (int i = 0; i < SuppProducts.Count; i++)
            {
                productsids.Add(SuppProducts[i].ProductID);
                productsnames.Add(SuppProducts[i].ProductName);
                productsprices.Add(SuppProducts[i].ProductPrice);
            }

            ViewData["productsids"] = productsids;
            ViewData["productsnames"] = productsnames;
            ViewData["productsprices"] = productsprices;

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
    
        public async Task<IActionResult> Create([Bind("SupplierID,SupplierName,SupplierPhonNumber")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Successfuly", "Admin");
            }
            return RedirectToAction("unSuccessfuly", "Admin");
            //return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }


          [HttpPost]
        
        public async Task<IActionResult> Edit( [Bind("SupplierID,SupplierName,SupplierPhonNumber")] Supplier supplier)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.SupplierID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index),new {token= "21232F297A57A5A743894A0E4A801FC3" });
                return RedirectToAction("Successfuly", "Admin");

                //return RedirectToAction("AdminPage","Admin",new { token = "21232F297A57A5A743894A0E4A801FC3" });
                //return View("AdminPage", "Admin");
            }
            return RedirectToAction("unSuccessfuly", "Admin");
            //return View(supplier);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier
                .FirstOrDefaultAsync(m => m.SupplierID == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

  
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Supplier.FindAsync(id);
            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Successfuly", "Admin");
        }


        private bool SupplierExists(int id)
        {
            return _context.Supplier.Any(e => e.SupplierID == id);
        }
    }
}
