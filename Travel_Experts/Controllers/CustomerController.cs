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
        [Authorize]

        
        public ActionResult Profile()
        {


               int CustomerId = (int)HttpContext.Session.GetInt32("CustomerId");
            //int CustomerId = 119;

            try
                {
                    Customer customer = CustomerManager.getCustomer(CustomerId);
                    List<Booking> bookings = CustomerManager.getCustomerBookings(CustomerId);

                    var cvm = new CustomerViewModel
                    {
                        cBookings = bookings,
                        Customer = customer,
                        ActiveCategory = "F",
                        LoadMode = "List"
                    };

                    
                    //load edit view in create mode
                    return View("Profile",cvm);

                    //load edit view in create mode
                    //return View("Edit", new Incident());
                }
                catch (Exception)
                {
                TempData["alert"] = "alert-danger";
                TempData["message"] = "Error loading page to show customer details.";
                    return View();
                }

            
            
        }
 /*      // [Route("[controller]s/{id?}")]
        public ActionResult Index(string id = "F")//uses view model
        {
            int CustomerId = (int)HttpContext.Session.GetInt32("CustomerId");
            List<Booking> bookings = null;


            Customer customer = CustomerManager.getCustomer(CustomerId);
            try
            {
                if (id == "F")//future bookings - can be deleted
                    bookings = CustomerManager.getCustomerBookings(CustomerId).Where(b=>b.BookingDate> DateTime.Today).ToList();

                else if (id == "P")//open incidents - date closed is null
                {
                    bookings = CustomerManager.getCustomerBookings(CustomerId).Where(b => b.BookingDate > DateTime.Today).ToList();
                }


            }
            catch (Exception)
            {
                ViewBag.Message = "Database error getting Incidents data.";
            }

            var cvm = new CustomerViewModel
            {
                cBookings = bookings,
                Customer = customer,
                ActiveCategory = id,
                LoadMode = "List"
                
            };
            return View(cvm);

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
        }*/

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                
                ViewBag.Action = "Edit"; //to indicate we are editing an existing recrod.
                Customer customer = CustomerManager.getCustomer(id);
                return View("EditProfile",customer);
            }
            catch
            {
                ViewBag.Message = "Error loading page to edit customer.";
                return View();
            }
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
