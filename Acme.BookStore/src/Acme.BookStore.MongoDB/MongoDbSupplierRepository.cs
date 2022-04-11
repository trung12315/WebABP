using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Acme.BookStore.Suppliers;

namespace Acme.BookStore
{
    internal class MongoDbSupplierRepository : MongoDbRepository<BookStoreMongoDbContext, Supplier, Guid>,
        ISupplierRepository
    {
        public MongoDbSupplierRepository(
          IMongoDbContextProvider<BookStoreMongoDbContext> dbContextProvider
          ) : base(dbContextProvider)
        {
        }

        public async Task<Supplier> FindByNameAsync(string name)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(supplier => supplier.Name == name);
        }

        public async Task<List<Supplier>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Supplier, IMongoQueryable<Supplier>>(
                    !filter.IsNullOrWhiteSpace(),
                    supplier => supplier.Name.Contains(filter)
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Supplier>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
