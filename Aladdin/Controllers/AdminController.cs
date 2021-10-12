using Aladdin.Data;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> Successfuly()
        {
            return View("Successfuly", "Admin");
        }
        public async Task<IActionResult> Unsuccessfuly()
        {
            return View("Unsuccessfuly", "Admin");
        }


        [Authorize]
        public IActionResult AdminPage()
        {
            //ViewData["token"] = password;
            return View("AdminPage1","Admin");
        }
    }
}
