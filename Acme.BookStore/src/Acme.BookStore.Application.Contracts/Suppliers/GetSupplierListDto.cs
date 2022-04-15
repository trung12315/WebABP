using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Suppliers
{
    public class GetSupplierListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
