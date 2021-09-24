using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aladdin.Data;
using Aladdin.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Aladdin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AladdinContext _context;
        private readonly IWebHostEnvironment _webhost;

        public ProductsController(AladdinContext context, IWebHostEnvironment webhost)
        {
            _context = context;
            _webhost = webhost;

        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        /*
        public string Details(int? id)
        {
            if (id == null)
            {
                return "NotFound()";
            }

            var product =  _context.Product
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return "NotFound()";
            }

            return "hello1111111";
        }

        */

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // I edited the create function so we can add image to product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,ProductSize,ProductColor,ProductRating,ProductPrice,ProductImage")] ProductView model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                Product product = new Product
                {
                    ProductID = model.ProductID,
                    ProductName = model.ProductName,
                    ProductQuantityS = model.ProductSize,
                    ProductColor = model.ProductColor,
                    ProductRating = model.ProductRating,
                    ProductPrice = model.ProductPrice,
                    ProductImage = uniqueFileName
                };

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,ProductSize,ProductColor,ProductRating,ProductPrice,ProductImage")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductID == id);
        }

        private string UploadedFile(ProductView model)
        {
            string uniqueFileName = null;

            if (model.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(_webhost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
