using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Alladin.Data;
using Alladin.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Alladin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AlladinContext _context;
        private readonly IWebHostEnvironment _webhost;

        public ProductsController(AlladinContext context, IWebHostEnvironment webhost)
        {
            _context = context;
            _webhost = webhost;

        }

        // GET: Products
        public async Task<IActionResult> Index(string searchSize, string searchString)
        {
            var products_list = from p in _context.Product select p;
            var products_list1 = _context.Product;
            var x = products_list1.AsQueryable();

            var products_list_sizes = from p in _context.Product select p.ProductSize;
            products_list_sizes = products_list_sizes.Distinct();
            if (!String.IsNullOrEmpty(searchString))
            {
                products_list = products_list.Where(s => s.ProductName.Contains(searchString));
                x = products_list.Where(s => s.ProductName.Contains(searchString));
                
            }

            if (!String.IsNullOrEmpty(searchSize))
            {
                x = x.Where(s => s.ProductSize.ToString() == searchSize);
                var t = x;
            }

                ViewData["AAA"] = products_list_sizes.ToList();
            return View(await x.ToListAsync());
        }



        public async Task<IActionResult> Search(string searchString)
        {
            var products_list = from p in _context.Product select p;
            var products_list1 = _context.Product;
            var x = products_list1.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                products_list = products_list.Where(s => s.ProductName.Contains(searchString));
                x = products_list.Where(s => s.ProductName.Contains(searchString));
            }
            return View(await x.ToListAsync());
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
                    ProductSize = model.ProductSize,
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
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,ProductSize,ProductColor,ProductRating,ProductPrice")] Product product)
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
