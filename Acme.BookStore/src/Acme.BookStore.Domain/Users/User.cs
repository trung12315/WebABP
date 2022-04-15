using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Users
{
    public class User :  FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
    public int Phone { get; set; }

    public string Address { get; set; }

    private User()
    {
    }
    internal User(
        Guid id,
        [NotNull] string name,
        [NotNull] int phone,
        [NotNull] string address)
        : base(id)
    {
        SetName(name);
        Phone = phone;
        Address = address;
    }
    internal User ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: UserConsts.MaxNameLength);
    }



}
    
}