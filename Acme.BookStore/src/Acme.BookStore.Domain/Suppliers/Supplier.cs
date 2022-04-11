using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Suppliers
{
    public class Supplier : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public int Phone { get; set; }

        public string Address { get; set; }
        
        private Supplier()
        { 
        }
        internal Supplier ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }
        internal Supplier(Guid id, [NotNull] string name, [NotNull] int phone ,[NotNull] string address):base(id)
        {
            SetName(name);
            Phone = phone;
            Address = address;
        }
        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: PupplierConsts.MaxNameLength);          
        }

  

    }
    
}
