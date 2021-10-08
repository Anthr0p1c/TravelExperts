using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel_Experts.Models
{
    public class BookingManager
    {

        public static Booking GetBooking(int BkgId)
        {
           TravelExpertsContext db = new TravelExpertsContext();
            Booking booking = db.Bookings.Include(b=>b.Package).Where(b=>b.BookingId==BkgId).FirstOrDefault();
            return booking;
        }//end get booking
        public static void updateBooking(Booking booking)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            Booking oBooking = db.Bookings.Find(booking.BookingId);//find existing data based on id from new object. 
            oBooking.TravelerCount = booking.TravelerCount;//assign new data to existing object 
            db.SaveChanges();
        }//end update
        public static void delBooking(int bookingId)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            Booking oBooking = db.Bookings.Find(bookingId);//find existing data based on id from new object. 
            BookingDetail bookingDetail = db.BookingDetails.Find(bookingId);
            if (bookingDetail != null)
                db.BookingDetails.Remove(bookingDetail);
            db.Bookings.Remove(oBooking);
            db.SaveChanges();
        }//end delete
    }
}
