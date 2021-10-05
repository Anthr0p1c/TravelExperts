using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Travel_Experts.Models
{
    public class WebCustomer
    {
        [Key]
        public int LoginId { get; set; }
        [Required]
        [StringLength(25)]
        public string Email { get; set; }
        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25)]
        public string LastName { get; set; }
        [Required]
        protected string Password { get; set; }
    }
}
