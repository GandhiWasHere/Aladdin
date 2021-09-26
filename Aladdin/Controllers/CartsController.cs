using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aladdin.Data;
using Aladdin.Models;

namespace Aladdin.Controllers
{
    public class CartsController : Controller
    {
        private readonly AladdinContext _context;

        public CartsController(AladdinContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cart.ToListAsync());
        }
<<<<<<< HEAD


        // GET: Carts/Details/5
        public async Task<IActionResult> mycart(int? id)
=======
        public IActionResult mycart()
        {
            return View();
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> mycart1(int? id)
>>>>>>> 2d20bc529dbb0b3d26d18b0a3585298a81d4862f
        {
            if (id == null)
            {
                return NotFound();
            }
<<<<<<< HEAD
            var cart = await _context.Cart.Include(x => x.CartProducts).SingleOrDefaultAsync(m => m.CartID == id);

=======

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.CartID == id);
>>>>>>> 2d20bc529dbb0b3d26d18b0a3585298a81d4862f
            if (cart == null)
            {
                return NotFound();
            }
<<<<<<< HEAD
            
            return View(cart);

=======

            return View(cart);
>>>>>>> 2d20bc529dbb0b3d26d18b0a3585298a81d4862f
        }


        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartID,CustomerID,CartTotal")] Cart cart)
        {
            if (id != cart.CartID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.CartID))
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
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.CartID == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.CartID == id);
        }
<<<<<<< HEAD
        [HttpGet]
        public async Task<IActionResult> AddProductAsync(int cartid, int productid)
        {
            Product p = _context.Product.Where(s => s.ProductID == productid).FirstOrDefault();
            Cart c = _context.Cart.Where(s => s.CartID == cartid).FirstOrDefault();
            if (c != null) // checks if c exsists
            {
                if (c.CartProducts == null)
                {
                    c.CartProducts = new List<Product>();
                    c.CartProducts.Add(p);
                }
                else c.CartProducts.Add(p);
                _context.Update(c);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();   
        }
=======
>>>>>>> 2d20bc529dbb0b3d26d18b0a3585298a81d4862f
    }
}
