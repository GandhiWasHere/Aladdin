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

        public async Task<IActionResult> Mycart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cart = await _context.Cart.Include(x => x.CartProducts).SingleOrDefaultAsync(m => m.CartID == id);

            if (cart == null)
            {
                return NotFound();
            }
            
            return View(cart);

        }

        [HttpGet]
        public async Task<IActionResult> AddProductAsync(int cartid, int productid, String size)
        {
            Product p = _context.Product.Where(s => s.ProductID == productid).FirstOrDefault();
            Cart c = _context.Cart.Where(s => s.CartID == cartid).Include(p=>p.CartProducts).FirstOrDefault();
            
            if (c != null) // checks if c exsists
            {
                if (c.CartProducts == null)
                {
                    c.CartProducts = new List<ProductInCart>();
                }
                ProductInCart p_copy = CheckInCart(c, productid);
                if (p_copy == null)
                {
                    p_copy = new()
                    {
                        ProductID = p.ProductID,
                        ProductColor = p.ProductColor,
                        ProductRating = p.ProductRating,
                        ProductImage = p.ProductImage,
                        ProductPrice = p.ProductPrice,
                        ProductName = p.ProductName,
                        ProductQuantityS = 0,
                        ProductQuantityM = 0,
                        ProductQuantityL = 0
                    };
                }
                if (size == "L")
                {
                    if (p.ProductQuantityL == 0)
                    {
                        return NotFound();
                    }
                    p_copy.ProductQuantityL += 1;
                    p.ProductQuantityL -= 1;
                }
                if (size == "M")
                {
                    if (p.ProductQuantityM == 0)
                    {
                        return NotFound();
                    }
                    p_copy.ProductQuantityM += 1;
                    p.ProductQuantityM -= 1;
                }

                if (size == "S")
                {
                    if (p.ProductQuantityS == 0)
                    {
                        return NotFound();
                    }
                    p_copy.ProductQuantityS += 1;
                    p.ProductQuantityS -= 1;
                }


                c.CartProducts.Add(p_copy);
                _context.Update(c);
                _context.Update(p);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();   
        }
        public ProductInCart CheckInCart(Cart cart, int id)
        {
            foreach (ProductInCart prod in cart.CartProducts)
            {
                if (prod.ProductID == id) return prod;
            }
            return null;
        }
        [HttpGet]
        public async Task<IActionResult> DeleteItemFromCart(int cartid, int productid)
        {
            Cart c = _context.Cart.Where(s => s.CartID == cartid).Include(p => p.CartProducts).FirstOrDefault();
            ProductInCart p = CheckInCart(c, productid);
            if (p != null)
            {
                Product product = _context.Product.Where(s => s.ProductID == p.ProductID).FirstOrDefault();
                product.ProductQuantityL += p.ProductQuantityL;
                product.ProductQuantityM += p.ProductQuantityM;
                product.ProductQuantityS += p.ProductQuantityS;
                c.CartProducts.Remove(p);
            }
            _context.Update(c);
            await _context.SaveChangesAsync();
            return RedirectToAction("mycart","carts", new { id = cartid });
        }

        [HttpGet]
        public async Task<IActionResult> CheckoutAsync(int cartid)
        {
            Cart c = _context.Cart.Where(s => s.CartID == cartid).Include(p => p.CartProducts).FirstOrDefault();
            foreach (ProductInCart pic in c.CartProducts)
            {
                Product product = _context.Product.Where(s => s.ProductID == pic.ProductID).FirstOrDefault();
                Sell sell = new() { ProductID=pic.ProductID, Quantity=(pic.ProductQuantityL + pic.ProductQuantityM + pic.ProductQuantityS), CustomerID=cartid, PDate= DateTime.Now};
                _context.Update(sell);
            }
            c.CartProducts = null;
            _context.Update(c);
            await _context.SaveChangesAsync();
            return View();
        }
    }

}
