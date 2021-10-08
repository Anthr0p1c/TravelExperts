using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Travel_Experts.Models;


namespace Travel_Experts.Controllers
{
    public class BookingController : Controller
    {
        // GET: edit a product - get
        public ActionResult EditBooking(int id)
        {
            try
            {
                
                Booking booking = BookingManager.GetBooking(id);
                return View(booking);
            }
            catch
            {
                TempData["alert"] = "alert-danger";
                TempData["message"] = "Error loading page to edit booking.";
                return View();
            }
        }//end edit booking
         // GET: delete a product - Get
        public ActionResult DelBooking(int id)
        {
            
            try
            {
                Booking booking = BookingManager.GetBooking(id);
                if (booking == null)
                {
                    TempData["alert"] = "alert-danger";
                    TempData["message"] = "Booking not found!";
                    return View();
                }
                else
                    
                return View(booking);
            }
            catch
            {
                TempData["alert"] = "alert-danger";
                TempData["message"] = "Error loading page to delete a booking.";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToActionResult EditBooking(Booking booking)
        {
            if (ModelState.IsValid)
                try
                {
                    if (booking.BookingId > 0)//create modee
                    {
                        //string Name = booking.Package.PkgLocation;{Name}

                        BookingManager.updateBooking(booking);
                        TempData["alert"] = "alert-success";
                        TempData["message"] = "Your booking has changed.";
                    }
                    
                    return RedirectToAction("Profile", "Customer");//if successful go back to the list

                }
                catch
                {
                    TempData["alert"] = "alert-danger";
                    TempData["message"] = "An error has occurred when trying to save changes.";
                    return RedirectToAction("EditBooking", "Booking");
                }
            else
                return RedirectToAction("EditBooking", "Booking");
        }// end update

        // POST: delete a product - post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToActionResult DelBooking(int id, Booking booking)
        {
            try
            {
                Booking dBooking = BookingManager.GetBooking(id);
                if (dBooking != null && dBooking.BookingId > 0)
                {
                    string Name = dBooking.Package.PkgLocation;
                    BookingManager.delBooking(id);
                    TempData["alert"] = "alert-success";
                    TempData["message"] = $"Your booking to {Name} has been cancelled";
                    return RedirectToAction("Profile", "Customer");
                }
                else
                {
                    TempData["alert"] = "alert-danger";
                    TempData["message"] = "Booking not found!";
                    return RedirectToAction("DelBooking", "Booking");
                }
            }
            catch
            {
                TempData["alert"] = "alert-danger";
                TempData["message"] = "We could not complete your request!";
                return RedirectToAction("DelBooking", "Booking");
            }
        }//end delete


    }
}
