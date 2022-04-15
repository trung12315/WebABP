using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Users
{
    public class UserAppService:BookStoreAppService, IUserAppService
    {
        private readonly IUserRepository _userRepository;
    private readonly UserManager _userManager;

    public UserAppService(IUserRepository supplierRepository, UserManager supplierManager)
    {
            _userRepository = supplierRepository;
            _userManager = supplierManager;
    }
    public async Task<UserDto> GetAsync(Guid id)
    {
        var supplier = await _userRepository.GetAsync(id);
        return ObjectMapper.Map<User, UserDto>(supplier);
    }
    public async Task<PagedResultDto<UserDto>> GetListAsync(GetUserListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(User.Name);
        }

        var suppliers = await _userRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = input.Filter == null
            ? await _userRepository.CountAsync()
            : await _userRepository.CountAsync(
                supplier => supplier.Name.Contains(input.Filter));

        return new PagedResultDto<UserDto>(
            totalCount,
            ObjectMapper.Map<List<User>, List<UserDto>>(suppliers)
        );
    }
}
}
