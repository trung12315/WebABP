using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Suppliers
{
    public class SupplierDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
    }
}
