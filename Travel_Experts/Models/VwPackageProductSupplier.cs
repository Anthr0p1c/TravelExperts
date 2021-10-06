using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Travel_Experts
{
    [Keyless]
    public partial class VwPackageProductSupplier
    {
        public int PackageId { get; set; }
        public int ProductSupplierId { get; set; }
        public int? ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProdName { get; set; }
        public int? SupplierId { get; set; }
        [StringLength(255)]
        public string SupName { get; set; }
    }
}
