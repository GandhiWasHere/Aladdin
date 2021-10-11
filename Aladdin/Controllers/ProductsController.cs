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
            if (!String.IsNullOrEmpty(searchColor) & (searchColor!="All"))
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
                    //return View(await _context.Customer.ToListAsync());
                          
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
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,ProductSize,ProductColor,ProductRating,ProductPrice,SupplierID,ProductImage")] ProductView model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                Product product = new()
                {
                    ProductID = model.ProductID,
                    ProductName = model.ProductName,
                    ProductQuantityS = model.ProductSize,
                    ProductColor = model.ProductColor,
                    ProductRating = model.ProductRating,
                    ProductPrice = model.ProductPrice,
                    SupplierID = model.SupplierID,
                    ProductImage = uniqueFileName
                    
                };

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }*/



        [HttpPost]
        //public async Task<IActionResult> Create([Bind("ProductID,ProductName,ProductColor,ProductSize,ProductRating,ProductPrice,SupplierID,ProductImage")] ProductView model)
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
                //return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("unSuccessfuly", "Admin");
            //return View(model);
        }







        
        [HttpPost]
        public string SaveImage(string file)
        {
            var x = file;
            var x1 = file;
            var t = x.Substring(22);  // remove data:image/png;base64,

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
            //var pa = filePath.Substring(57);  // remove data:image/png;base64,
            return filenameFinal;
        
        
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
        /*        [HttpPost]
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
        */


        [HttpPost]

        //public async Task<IActionResult> Edit([Bind("ProductID,ProductName,ProductSize,ProductColor,ProductRating,ProductPrice,ProductImage")] Product product)
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
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Successfuly", "Admin");
            }
            return RedirectToAction("unSuccessfuly", "Admin");
            //return View(product);
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
