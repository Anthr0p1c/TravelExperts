using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;

namespace Travel_Experts.Models
{
    public class CustomerManager
    {
        public static int addCustomer(Customer customer)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            db.Customers.Add(customer);
            db.SaveChanges();
            int CustId = customer.CustomerId;
            return CustId;
        }//add customer

        public static Customer getCustomer(int CustId)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            Customer customer = db.Customers.Find(CustId);
            return customer;
        }
        public static Customer getCustomerbyEmail(string Email)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            Customer customer = db.Customers.Where(c=>c.CustEmail.ToLower()==Email.ToLower()).FirstOrDefault();
            return customer;
        }
<<<<<<< Updated upstream
 
=======

        public static List<Booking> getCustomerBookings(int CustId)
        {
            List<Booking> bookings;
            TravelExpertsContext db = new TravelExpertsContext();
            bookings = db.Bookings.Include(p=>p.Package).Where(b => b.CustomerId == CustId).OrderByDescending(b=>b.BookingId).ToList();
            return bookings;
        }


>>>>>>> Stashed changes
    }
}
