using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Travel_Experts.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Travel_Experts.Controllers
{
    public class HomeController : Controller
    {
        private readonly TravelExpertsContext _context;

        public HomeController(TravelExpertsContext context)
        {
            _context = context;
        }

        // GET: Packages
        public async Task<IActionResult> Index(string location, string type)
        {
            var packages = await _context.Packages.Include(i => i.Bookings).ToListAsync(); 
            if (location != null)
                packages = await _context.Packages.Where(i => (i.PkgLocation == location)).ToListAsync();
            if (type != null)
                packages = await _context.Packages.Where(i => (i.PkgType == type)).ToListAsync();

            // Find packages selections
            ViewData["PkgLocation"] = new SelectList(_context.Packages, "PkgLocation", "PkgLocation");
            ViewData["PkgStartDate"] = new SelectList(_context.Packages, "PkgStartDate", "PkgStartDate");
            ViewData["PkgType"] = new SelectList(_context.Packages, "PkgType", "PkgType");

            return View(packages);           
        }


        //GET: Package check out
        public async Task<IActionResult> CheckOut(int? id)
        {
            if(HttpContext.Session.GetInt32("PackageId") != null)
            {
                id = HttpContext.Session.GetInt32("PackageId");
                var package = await _context.Packages
                   .FirstOrDefaultAsync(m => m.PackageId == id);

                return View(package);
            }

            TempData["message"] = $"Your cart is empty!";
            TempData["alert"] = "alert-danger";
            return RedirectToAction("Index");
        }


        //POST: Package purchase
        [HttpPost]
        public IActionResult CheckOut([Bind("PackageId", "PkgName")] Package package)
        {
            //var booking = await _context.Bookings.Add()

            TempData["message"] = $"{package.PkgName} was successfully purchased!";
            TempData["alert"] = "alert-success";

            HttpContext.Session.SetInt32("PurchasedPackage", package.PackageId);

            return RedirectToAction("Index");
        }

        //POST: Register
/*        [HttpPost]
        public IActionResult Register([Bind("email", "fname", "lname", "password")] User user)
        {
            return RedirectToAction("Index");
        }



        //POST: Login
        [HttpPost]
        public IActionResult Login([Bind("email", "password")] UserManager Authenticate)
        {
            return RedirectToAction("Index");
        }

*/

        [HttpPost]
        //POST: Find
        public IActionResult Find([Bind("PkgLocation", "PkgStartDate", "PkgType")] Package package)
        {
            string PkgLocation = package.PkgLocation;
            string PkgType = package.PkgType;
            return RedirectToAction("Index", new { location = PkgLocation, @type = PkgType });
        }

        //POST: Register
        [HttpPost]
        public IActionResult Register([Bind("email", "fname", "lname", "password")] WebCustomer register)
        {
            return RedirectToAction("Index");
        }

        //POST: Login
        [HttpPost]
        public IActionResult Login([Bind("email", "fname", "lname", "password")] WebCustomer login)
        {
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
