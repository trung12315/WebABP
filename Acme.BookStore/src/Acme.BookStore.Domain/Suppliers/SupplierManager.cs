using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Acme.BookStore.Suppliers;

public class SupplierManager : DomainService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierManager(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<Supplier> CreateAsync(
        [NotNull] string name,
        [NotNull] int  phone,
        [NotNull] string address)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingAuthor = await _supplierRepository.FindByNameAsync(name);
        if (existingAuthor != null)
        {
            throw new SupplierAlreadyExistsException(name);
        }

        return new Supplier(
            GuidGenerator.Create(),
            name,
            phone,
            address
        );
    }

    public async Task ChangeNameAsync(
        [NotNull] Supplier supplier,
        [NotNull] string newName)
    {
        Check.NotNull(supplier, nameof(supplier));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingAuthor = await _supplierRepository.FindByNameAsync(newName);
        if (existingAuthor != null && existingAuthor.Id != supplier.Id)
        {
            throw new SupplierAlreadyExistsException(newName);
        }

        supplier.ChangeName(newName);
    }
}

