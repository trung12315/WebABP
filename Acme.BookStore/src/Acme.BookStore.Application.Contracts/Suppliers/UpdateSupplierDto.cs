using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.Suppliers
{
    public class UpdateSupplierDto
    {
        [Required]
        [StringLength(SupplierConsts.MaxNameLength)]
        public string Name { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
