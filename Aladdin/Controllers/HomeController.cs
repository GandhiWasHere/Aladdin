using Aladdin.Data;
using Aladdin.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Aladdin.Controllers
{
    public class HomeController : Controller
    {
        private readonly AladdinContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AladdinContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string txtUserName, string txtPassword)
        {
            
            //create a cookie
            if ((txtPassword == "admin") && (txtUserName == "admin"))
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, txtUserName)
                };
                var id = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(id);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                //return RedirectToAction("", "Admin");
                return RedirectToAction("AdminPage", "Admin");
            }
            else
            {
                Customer userDetails = _context.Customer.Where(x => x.CustomerName == txtUserName && x.CustomerPassword == txtPassword).FirstOrDefault();
                if (userDetails != null)
                {
                    HttpContext.Response.Cookies.Append("cart_id", userDetails.CartID.ToString());
                    return RedirectToAction("Customer", "Details", userDetails.CustomerID);
                }
                else
                {
                    ViewData["Error_1"]= "WrongUsername\\password";
                    return View();
                }
                    
            }

        }
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
