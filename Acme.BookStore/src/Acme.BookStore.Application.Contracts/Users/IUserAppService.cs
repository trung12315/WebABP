using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task<UserDto> GetAsync(Guid id);

        Task<PagedResultDto<UserDto>> GetListAsync(GetUserListDto input);

        //Task<UserDto> CreateAsync(CreateSupplierDto input);

        //Task UpdateAsync(Guid id, UpdateSupplierDto input);

        //Task DeleteAsync(Guid id);
    }
}