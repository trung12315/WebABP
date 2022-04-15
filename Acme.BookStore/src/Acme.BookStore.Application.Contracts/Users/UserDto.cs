using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Users
{
    public class UserDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
