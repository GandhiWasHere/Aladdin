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
                    p_copy.ProductQuantityL += 1;
                if (size == "M")
                    p_copy.ProductQuantityL += 1;
                if (size == "S")
                    p_copy.ProductQuantityL += 1;

                c.CartProducts.Add(p_copy);
                _context.Update(c);
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
    }

}
