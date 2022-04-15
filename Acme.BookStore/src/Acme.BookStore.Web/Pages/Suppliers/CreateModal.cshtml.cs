using Acme.BookStore.Suppliers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Acme.BookStore.Web.Pages.Suppliers
{
    public class CreateModalModel : BookStorePageModel
    {
        [BindProperty]
        public CreateSupplierViewModel Supplier { get; set; }

        private readonly ISupplierAppService _supplierAppService;

        public CreateModalModel(ISupplierAppService supplierAppService)
        {
            _supplierAppService = supplierAppService;
        }

        public void OnGet()
        {
            Supplier = new CreateSupplierViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateSupplierViewModel, CreateSupplierDto>(Supplier);
            await _supplierAppService.CreateAsync(dto);
            return NoContent();
        }

        public class CreateSupplierViewModel
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
}
