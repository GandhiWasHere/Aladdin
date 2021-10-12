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
using System.Text;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Index(string searchColor, string searchString, string minValue, string maxValue, string Rating)
        {
            var products_list = from p in _context.Product select p;
            var colors = from p in _context.Product select p.ProductColor;
            colors = colors.Distinct();
            if (!String.IsNullOrEmpty(searchString))
            {
                products_list = products_list.Where(s => s.ProductName.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchColor) & (searchColor!="Color"))
            {
                products_list = products_list.Where(s => s.ProductColor == searchColor);
            }
            if (!String.IsNullOrEmpty(minValue))
            {
                products_list = products_list.Where(s => s.ProductPrice >= Int32.Parse(minValue));
            }
            if (!String.IsNullOrEmpty(maxValue))
            {
                products_list = products_list.Where(s => s.ProductPrice <= Int32.Parse(maxValue));
            }
            if (!String.IsNullOrEmpty(Rating))
            {
                products_list = products_list.Where(s => s.ProductRating >= Int32.Parse(Rating));
            }
            return View(await products_list.ToListAsync());
        }


        // GET: Products
        [Authorize]
        public async Task<IActionResult> Prodadmin( )
        {
                    var products_list = from p in _context.Product select p;
                    return View(await products_list.ToListAsync());
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

        [Authorize]
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,ProductColor,ProductRating,ProductPrice,SupplierID,ProductImage")] Product model)
        {
            if (ModelState.IsValid)
            {
                //string uniqueFileName = UploadedFile(model);
                Product product = new()
                {
                    ProductID = model.ProductID,
                    ProductName = model.ProductName,
                    ProductQuantityS = model.ProductQuantityS,
                    ProductQuantityM = model.ProductQuantityM,
                    ProductQuantityL = model.ProductQuantityL,
                    ProductColor = model.ProductColor,
                    ProductRating = model.ProductRating,
                    ProductPrice = model.ProductPrice,
                    SupplierID = model.SupplierID,
                    ProductImage = model.ProductImage
                };

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Successfuly", "Admin");
                
            }
            return RedirectToAction("unSuccessfuly", "Admin");

        }


        
        [HttpPost]
        public string SaveImage(string file)
        {
            
            var x = file;
            

            var se2 = x.Split("base64,");
            var t = se2[1];
            byte[] bytes = Convert.FromBase64String(t);

            // Requires System.IO
            
            string uniqueFileName = "";
            string uploadsFolder = Path.Combine(_webhost.WebRootPath, "images");
            Random r = new Random();
            int num = r.Next();
            uniqueFileName = Guid.NewGuid().ToString() + "_" + num.ToString() + ".png";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            System.IO.File.WriteAllBytes(filePath, bytes);

            var se = filePath.Split("\\");
            var filenameFinal = se[se.Length -1];
            return filenameFinal;
        
        
        }






        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("ProductID,ProductName,ProductColor,ProductSize,SupplierID,ProductRating,ProductPrice,ProductImage,ProductQuantityS,ProductQuantityM,ProductQuantityL")] Product product)
        {
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
                
                return RedirectToAction("Successfuly", "Admin");
            }
            return RedirectToAction("unSuccessfuly", "Admin");
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
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
*/





        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Successfuly", "Admin");
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
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.ProductImage.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}
