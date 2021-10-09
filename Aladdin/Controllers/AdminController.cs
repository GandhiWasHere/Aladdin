using Aladdin.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aladdin.Controllers
{
    public class AdminController : Controller
    {

        private readonly AladdinContext _context;
        private readonly IWebHostEnvironment _webhost;

        public AdminController(AladdinContext context, IWebHostEnvironment webhost)
        {
            _context = context;
            _webhost = webhost;

        }
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult AdminPage(string password)
        {
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
            if (s == password)
            {
                var x = from p in _context.Product select p;
                ViewData["SSS"] = x;
                ViewData["token"] = password;

                return View("AdminPage1","Admin");
            }
            return View("Index");
        }
/*        public async Task<IActionResult> ProductsIndexAdmin(string searchColor, string searchString)
        {
            var products_list = from p in _context.Product select p;
            var colors = from p in _context.Product select p.ProductColor;
            colors = colors.Distinct();
            if (!String.IsNullOrEmpty(searchString))
            {
                products_list = products_list.Where(s => s.ProductName.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchColor) & (searchColor != "All"))
            {
                products_list = products_list.Where(s => s.ProductColor == searchColor);
            }
            return View(await products_list.ToListAsync());

        }*/
            /*        public string AdminPage(string password)
                    {
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
                        if(s == password)
                        {
                            return "good3333";
                        }
                        return "BAD3333";
                    }*/

            public string Login(string username,string password)
        {


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
            if (username == "admin" && password =="admin")
            {
                var s = CreateMD5("admin");
                return s;
            }
            return "BAD";
        }
    }
}
