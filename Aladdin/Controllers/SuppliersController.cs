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

namespace Aladdin.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly AladdinContext _context;

        public SuppliersController(AladdinContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        /*public async Task<IActionResult> Index()
        {
            //only if you are admin !

            

            return View(await _context.Supplier.ToListAsync());
        }

*/




        public async Task<IActionResult> Index(string token)
        {
            //only if you are admin !
            static string CreateMD5(string input)
            {
                // Use input string to calculate MD5 hash
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    // Convert the byte array to hexadecimal string
                    System.Text.StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("X2"));
                    }
                    return sb.ToString();
                }
            }
            var s = CreateMD5("admin");
            if (s == token)
            {

                return View(await _context.Supplier.ToListAsync());
            }

            return RedirectToAction("index", "Products");
            //return View("Index");



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
            /*  var supplier1 = await _context.Supplier
                  .FirstOrDefaultAsync(m => m.SupplierID == id).include;
              var SuppProducts = from p in _context.Supplier.AsQueryable();*/




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

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierID,SupplierName,SupplierPhonNumber")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

*/




        [HttpPost]
    
        public async Task<IActionResult> Create([Bind("SupplierID,SupplierName,SupplierPhonNumber")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
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

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    /*    [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierID,SupplierName,SupplierPhonNumber")] Supplier supplier)
        {
            if (id != supplier.SupplierID)
            {
                return NotFound();
            }

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
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }
*/


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
                return RedirectToAction(nameof(Index),new {token= "21232F297A57A5A743894A0E4A801FC3" });
                //return RedirectToAction("AdminPage","Admin",new { token = "21232F297A57A5A743894A0E4A801FC3" });
                //return View("AdminPage", "Admin");
            }
            return View(supplier);
        }


        /*[HttpPost]
        public  void Edit([Bind("SupplierID,SupplierName,SupplierPhonNumber")] Supplier supplier)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                     _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.SupplierID))
                    {
                       //return "not found";
                        //return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return "update succefully";
                //return View("Suppliers", "Admin");
                //return RedirectToAction(nameof(Index));
            }
            //return "update unsuccefully";
            //return View(supplier);
        }*/











        // GET: Suppliers/Delete/5
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

        // POST: Suppliers/Delete/5
        /*        [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                       public async Task<IActionResult> DeleteConfirmed(int id)
                        {
                            var supplier = await _context.Supplier.FindAsync(id);
                            _context.Supplier.Remove(supplier);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }

        */
        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Supplier.FindAsync(id);
            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool SupplierExists(int id)
        {
            return _context.Supplier.Any(e => e.SupplierID == id);
        }
    }
}
