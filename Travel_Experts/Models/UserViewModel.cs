/* Purpose: A view model that combines necessary fields for User and Customer tables.
 * Used by: Register user
 * Date: 05Oct2021
 * Author: Priya P
 */
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Travel_Experts.Models
{
    public class UserViewModel
    {

 
        [Required(ErrorMessage = "Please enter your Email.")]
        [StringLength(50)]
        [Remote("VerifyEmail", "User", ErrorMessage="Email already exists.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your First Name.")]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name.")]
        [StringLength(25)]
        public string LastName { get; set; }

       
        [StringLength(75)]
        [Compare("cPassword")]
        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; }

        [StringLength(75)]
        [Required(ErrorMessage = "Please confirm your password.")]
        public string cPassword { get; set; }

    }
}
