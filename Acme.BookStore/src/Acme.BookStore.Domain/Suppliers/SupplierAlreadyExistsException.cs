using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Acme.BookStore.Suppliers
{
    internal class SupplierAlreadyExistsException : BusinessException
    {
        public SupplierAlreadyExistsException(string name)
            : base(BookStoreDomainErrorCodes.AuthorAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
