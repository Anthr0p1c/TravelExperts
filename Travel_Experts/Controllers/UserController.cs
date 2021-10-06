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


namespace Travel_Experts.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login(string returnUrl = null  )
        {
            if (returnUrl != null)
                TempData["ReturnUrl"] = returnUrl; // preserve to come back to this page
            return View();
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

                // if no return URl, go to the Index page of Rentals controller
                if (TempData["ReturnUrl"] != null)
                    return Redirect(TempData["ReturnUrl"].ToString());
                else
                    return RedirectToAction("Index", "Home");
            }
                else
                    return RedirectToAction("Index", "Home");
        }//LoginAsync

        [HttpPost]
        public RedirectToActionResult Register([Bind("Email", "FirstName", "LastName", "Password", "cPassword")] UserViewModel ouserViewModel)
        {


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
                return RedirectToAction("Index", "Home");
            }
            else
                return  RedirectToAction("form-container-register", "Home");
        }

        //client side validation of email
        /*       public JsonResult EmailExists(string Email)
               {
                   return Json(data: true, JsonRequestBehavior.AllowGet);
               }*/

        public async Task<IActionResult> LogoutAsync()
        {
            // remove the authentication cookie
            await HttpContext.SignOutAsync("Cookies");
            HttpContext.Session.SetString("Email", "");
            HttpContext.Session.SetInt32("CustomerId", 0);// no current customer
            return RedirectToAction("Index", "Home");
        }// LogoutAsync

    }//end controller
}//end namespace
