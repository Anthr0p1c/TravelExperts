using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Travel_Experts.Models
{
    public class UserManager
    {

        public static User Authenticate(string Email, string password)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            string ePassword = EncryptDecrypt.Encrypt(password);
            List<User> users = db.Users.ToList();
            User user = users.SingleOrDefault(u => u.Email == Email && u.Password == ePassword);
            return user;
        }// Authenticate

        public static int EmailExists(string email)
        {
            int iCount = 0;
            TravelExpertsContext db = new TravelExpertsContext();
            if (!string.IsNullOrEmpty(email))
            {
                iCount = db.Users.Where(u => u.Email.ToLower() == email.ToLower()).Count();
            }
            return iCount;
        }//Email exists
        public static void createUser(User user) 
        {
            TravelExpertsContext db = new TravelExpertsContext();
            db.Users.Add(user);
            db.SaveChanges();
        }//create user
 /*       public static int EmailExists( string email)
        {
            int iCount = 0;
            TravelExpertsContext db = new TravelExpertsContext();
            if (!string.IsNullOrEmpty(email))
            {
                iCount = db.Users.Where(c => c.CustEmail.ToLower() == email.ToLower() && c.CustomerID != CustId).Count();

            }
            return iCount;
        }*/
    }//end class
}//end namespace
