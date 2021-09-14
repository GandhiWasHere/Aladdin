using Alladin.Data;
using Alladin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;


namespace Alladin.Controllers
{
    public class LoginController : Controller
    {
        private readonly AlladinContext _context;

        public LoginController(AlladinContext context)
        {
            _context = context;
        }

        //GET: /login
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize([Bind("CustomerName,CustomerPassword")] Customer customer)
        {
            var userDetails= _context.Customer.Where(x => x.CustomerName == customer.CustomerName && x.CustomerPassword == customer.CustomerPassword).FirstOrDefault();
            if (userDetails == null)
            {
                customer.ErrorMessage = "Bad Username/password.";
                return View("Index", customer);
            }
            else
            {
                HttpContext.Session.SetString("sessionString", userDetails.CustomerName);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
