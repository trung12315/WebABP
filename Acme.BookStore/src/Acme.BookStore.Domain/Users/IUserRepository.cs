using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Users
{
    public interface IUserRepository: IRepository<User, Guid>
    {
        Task<User> FindByNameAsync(string name);

        Task<List<User>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}