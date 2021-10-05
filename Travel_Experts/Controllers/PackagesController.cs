using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Travel_Experts;
using Microsoft.AspNetCore.Http;

namespace Travel_Experts.Controllers
{
    public class PackagesController : Controller
    {
        private readonly TravelExpertsContext _context;

        public PackagesController(TravelExpertsContext context)
        {
            _context = context;
        }



        // GET: Packages for homepage cards
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Packages.Include(c => c.Bookings)
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }
            ViewData["TravelerCount"] = new SelectList(_context.Bookings, "TravelerCount", "TravelerCount");
            ViewBag.baseprice = ((int)package.PkgBasePrice);
            
            return View(package);
        }

        // POST: Package add to cart
        [HttpPost]
        public async Task<IActionResult> Details([Bind("PkgName", "PkgStartDate", "PkgEndDate", "PkgBasePrice", "PkgDesc", "PkgImageLocation")] Package package, [Bind("TravelerCount")] Booking booking)
        {
            
            if (ModelState.IsValid)
            {
                TempData["message"] = $"{package.PkgName} added to cart!";
                TempData["alert"] = "alert-success";

                if (HttpContext.Session.GetInt32("Count") != null)
                    HttpContext.Session.SetInt32("Count", ((int)HttpContext.Session.GetInt32("Count") + 1));
                else
                    HttpContext.Session.SetInt32("Count", 1);

                _context.Add(package);
                await _context.SaveChangesAsync();

                var newPackage = await _context.Packages.FirstOrDefaultAsync(m => m.PkgBasePrice == package.PkgBasePrice);
                HttpContext.Session.SetInt32("PackageId", newPackage.PackageId);
                //HttpContext.Session.SetInt32("TravelerCount", ((int)booking.TravelerCount));

                return RedirectToAction("Index", "Home", new { area = "" }); ;
            }

            return View("Details");
        }




        // GET: Packages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackageId,PkgName,PkgStartDate,PkgEndDate,PkgDesc,PkgBasePrice,PkgAgencyCommission")] Package package)
        {
            if (ModelState.IsValid)
            {
                _context.Add(package);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(package);
        }

        // GET: Packages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Packages.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PackageId,PkgName,PkgStartDate,PkgEndDate,PkgDesc,PkgBasePrice,PkgAgencyCommission")] Package package)
        {
            if (id != package.PackageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(package);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageExists(package.PackageId))
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
            return View(package);
        }

        // GET: Packages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.Packages
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var package = await _context.Packages.FindAsync(id);
            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(int id)
        {
            return _context.Packages.Any(e => e.PackageId == id);
        }
    }
}
