using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.BookStore.Users
{
    public class CreateUserDto
    {
        [Required]
        [StringLength(UserConsts.MaxNameLength)]
        public string Name { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
