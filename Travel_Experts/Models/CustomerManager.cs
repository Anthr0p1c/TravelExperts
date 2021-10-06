using System;
using System.Collections.Generic;
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
 
    }
}
