using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Acme.BookStore.Users
{
    public class UserManager : DomainService
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }

        public async Task<User> CreateAsync(
            [NotNull] string name,
            [NotNull] int phone,
            [NotNull] string address)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingSupplier = await _userRepository.FindByNameAsync(name);
            if (existingSupplier != null)
            {
                throw new UserAlreadyExistsException(name);
            }

            return new User(
                GuidGenerator.Create(),
                name,
                phone,
                address
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] User supplier,
            [NotNull] string newName)
        {
            Check.NotNull(supplier, nameof(supplier));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingSupplier = await _userRepository.FindByNameAsync(newName);
            if (existingSupplier != null && existingSupplier.Id != supplier.Id)
            {
                throw new UserAlreadyExistsException(newName);
            }

            supplier.ChangeName(newName);
        }
    }
}