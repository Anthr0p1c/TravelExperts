/* Purpose: controller class for User Login/Registration and customer account creation
 * Used by: Login/Register of Index page
 * Date: 05Oct2021
 * Author: Priya P
 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Travel_Experts.Models;
using System.Linq;

namespace Travel_Experts.Controllers
{
    public class UserController : Controller
    {

        private readonly TravelExpertsContext _context;

        public UserController(TravelExpertsContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View("Login");
        }
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
       
        public async Task<IActionResult> LoginAsync([Bind("Email", "Password")] User user)
        {
            User usr = UserManager.Authenticate(user.Email, user.Password);
            if (usr != null) //  authenticated
            {
                Customer customer = CustomerManager.getCustomerbyEmail(user.Email);
                HttpContext.Session.SetInt32("CustomerId", customer.CustomerId);
                HttpContext.Session.SetString("Email", usr.Email);

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, customer.CustEmail),
                    new Claim(ClaimTypes.Surname, customer.CustLastName),
                    new Claim(ClaimTypes.GivenName, customer.CustFirstName),
                    
                   
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies"); // authentication type: Cookies
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                // generate authentication cookie
                await HttpContext.SignInAsync("Cookies", principal);
                return Redirect(HttpContext.Session.GetString("Path"));

            }
                else
                    return View(user);
        }//LoginAsync

        [HttpPost]
        public IActionResult Register([Bind("Email", "FirstName", "LastName", "Password", "cPassword")] UserViewModel ouserViewModel)
        {
            User check = _context.Users.FirstOrDefault(t => t.Email == ouserViewModel.Email);
            if (check != null)
            {
                ModelState.AddModelError("Email",
                    $"The {ouserViewModel.Email} is already in the database.");
                return View(ouserViewModel);
            }

            if (ModelState.IsValid)
            {
                User nUser = new User();//create new user object 

                nUser.Email = ouserViewModel.Email;
                nUser.Password = EncryptDecrypt.Encrypt(ouserViewModel.Password);
                nUser.RoleId = "C";
                UserManager.createUser(nUser);
                Customer nCustomer = new Customer();
                nCustomer.CustFirstName = ouserViewModel.FirstName;
                nCustomer.CustLastName = ouserViewModel.LastName;
                nCustomer.CustEmail = ouserViewModel.Email;
                nCustomer.CustAddress = "";
                nCustomer.CustCity = "";
                nCustomer.CustCountry = "";
                nCustomer.CustPostal = "";
                nCustomer.CustBusPhone = "";
                nCustomer.CustHomePhone = "";
                nCustomer.CustProv = "";

                int CustomerId = CustomerManager.addCustomer(nCustomer);
                HttpContext.Session.SetInt32("CustomerId", CustomerId);
                HttpContext.Session.SetString("Email", ouserViewModel.Email);
                return Redirect(HttpContext.Session.GetString("Path"));
            }
            else
                return View(ouserViewModel);
        }

        [HttpPost]
   
        public JsonResult VerifyEmail(string Email)
        {
            if (_context.Users.Where(i => i.Email.ToLowerInvariant().Equals(Email.ToLower())) != null)
            {
                return Json("Invalid Error Message");
            }
            return Json(true);
        }

        //client side validation of email
        //public JsonResult VerifyEmail(string Email)
        //{
        //    bool isExist = _context.Users.Where(i => i.Email.ToLowerInvariant().Equals(Email.ToLower())) != null;

        //    return Json(!isExist, new Newtonsoft.Json.JsonSerializerSettings());
        //}

        public async Task<IActionResult> LogoutAsync()
        {
            // remove the authentication cookie
            HttpContext.Session.SetInt32("Count", 0);
            await HttpContext.SignOutAsync("Cookies");
            HttpContext.Session.SetString("Email", "");
            HttpContext.Session.SetInt32("CustomerId", 0);// no current customer
            return RedirectToAction("Index", "Home");
        }// LogoutAsync

    }//end controller
}//end namespace
