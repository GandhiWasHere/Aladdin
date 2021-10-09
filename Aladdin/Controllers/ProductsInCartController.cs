using Aladdin.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//remove thisS
namespace Aladdin.Controllers
{
    public class ProductsInCartController : Controller
    {
        private readonly AladdinContext _context;

        public ProductsInCartController(AladdinContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductInCart.ToListAsync());
        }


        // GET: ProductsInCartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsInCartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductsInCartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsInCartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductsInCartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductsInCartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductsInCartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
