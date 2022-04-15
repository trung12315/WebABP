using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
namespace Acme.BookStore.Suppliers
{
    //[Authorize(BookStorePermissions.Suppliers.Default)]
    public class SupplierAppService : BookStoreAppService, ISupplierAppService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly SupplierManager _suppliermanager;

        public SupplierAppService(ISupplierRepository supplierRepository, SupplierManager supplierManager)
        {
            _supplierRepository = supplierRepository;
            _suppliermanager = supplierManager;
        }
        public async Task<SupplierDto> GetAsync(Guid id)
        {
            var supplier = await _supplierRepository.GetAsync(id);
            return ObjectMapper.Map<Supplier, SupplierDto>(supplier);
        }
        public async Task<PagedResultDto<SupplierDto>> GetListAsync(GetSupplierListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Supplier.Name);
            }

            var suppliers = await _supplierRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _supplierRepository.CountAsync()
                : await _supplierRepository.CountAsync(
                    supplier => supplier.Name.Contains(input.Filter));

            return new PagedResultDto<SupplierDto>(
                totalCount,
                ObjectMapper.Map<List<Supplier>, List<SupplierDto>>(suppliers)
            );
        }
        //[Authorize(BookStorePermissions.Suppliers.Create)]
        public async Task<SupplierDto> CreateAsync(CreateSupplierDto input)
        {
            var supplier = await _suppliermanager.CreateAsync(
                input.Name,
                input.Phone,
                input.Address
            );

            await _supplierRepository.InsertAsync(supplier);

            return ObjectMapper.Map<Supplier, SupplierDto>(supplier);
        }
        //[Authorize(BookStorePermissions.Suppliers.Edit)]
        public async Task UpdateAsync(Guid id, UpdateSupplierDto input)
        {
            var author = await _supplierRepository.GetAsync(id);

            if (author.Name != input.Name)
            {
                await _suppliermanager.ChangeNameAsync(author, input.Name);
            }

            author.Phone = input.Phone;
            author.Address = input.Address;

            await _supplierRepository.UpdateAsync(author);
        }
        //[Authorize(BookStorePermissions.Suppliers.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _supplierRepository.DeleteAsync(id);
        }
    }
}
