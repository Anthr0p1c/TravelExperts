using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Travel_Experts
{
    public partial class Package
    {
        public Package()
        {
            Bookings = new HashSet<Booking>();
            PackagesProductsSuppliers = new HashSet<PackagesProductsSupplier>();
        }

        [Key]
        public int PackageId { get; set; }
        [Required]
        [StringLength(50)]
        public string PkgName { get; set; }
        [Column(TypeName = "datetime")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? PkgStartDate { get; set; }
        [Column(TypeName = "datetime")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? PkgEndDate { get; set; }
        [StringLength(50)]
        
        public string PkgDesc { get; set; }
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "${0:0.00}", ApplyFormatInEditMode = false)]
        public decimal PkgBasePrice { get; set; }
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "${0:0.00}", ApplyFormatInEditMode = false)]
        public decimal? PkgAgencyCommission { get; set; }

        [StringLength(50)]
        public string PkgImageLocation { get; set; }

        
        public string PkgType { get; set; }
        public string PkgLocation { get; set; }


        [InverseProperty(nameof(Booking.Package))]
        public virtual ICollection<Booking> Bookings { get; set; }
        [InverseProperty(nameof(PackagesProductsSupplier.Package))]
        public virtual ICollection<PackagesProductsSupplier> PackagesProductsSuppliers { get; set; }
    }
}
