using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Travel_Experts.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Travel_Experts.Controllers
{
    public class CustomerController : Controller
    {
        // GET: CustomerController
   //     [Authorize]
        public ActionResult Index()
        {
            int CustomerId =(int)HttpContext.Session.GetInt32("CustomerId");
            if (CustomerId>0)
            {
                try
                {
                    Customer customer = CustomerManager.getCustomer(CustomerId);
                    List<Booking> bookings = CustomerManager.getCustomerBookings(CustomerId);

                    var cvm = new CustomerViewModel
                    {
                        cBookings = bookings,
                        Customer = customer,
                        ActiveCategory = "Future",
                        LoadMode = "Details"
                    };

                    ViewBag.Message = "";
                    //load edit view in create mode
                    return View("Details", cvm);

                    //load edit view in create mode
                    //return View("Edit", new Incident());
                }
                catch (Exception)
                {
                    ViewBag.Message = "Error loading page to show customer details.";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
