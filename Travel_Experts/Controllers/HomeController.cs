using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;
using Travel_Experts.Models;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> Index()
        {
            var packages = await _context.Packages.Include(i => i.Bookings).ToListAsync();

            // Find packages selections
            ViewData["PkgName"] = new SelectList(_context.Packages, "PkgName", "PkgName");
            ViewData["PkgStartDate"] = new SelectList(_context.Packages, "PkgStartDate", "PkgStartDate");
            ViewData["PkgDesc"] = new SelectList(_context.TripTypes, "TripTypeId", "Ttname");

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
