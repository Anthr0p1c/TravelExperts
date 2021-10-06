using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Travel_Experts
{
    public partial class User
    {
        [Key]
        [Column("User_Id")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter your email.")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required (ErrorMessage = "Please enter a password.")]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [Column("Role_Id")]
        [StringLength(1)]
        public string RoleId { get; set; }
        [Column("Ref_Id")]
        public int? RefId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(UserRole.Users))]
        public virtual UserRole Role { get; set; }
    }
}
