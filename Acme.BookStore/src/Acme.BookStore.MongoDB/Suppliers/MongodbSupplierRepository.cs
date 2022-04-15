using Acme.BookStore.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Acme.BookStore.Suppliers
{
    public class MongodbSupplierRepository 
        : MongoDbRepository<BookStoreMongoDbContext, Supplier, Guid>,ISupplierRepository
    {
        public MongodbSupplierRepository(
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
               // .GroupBy(sorting)
                .As<IMongoQueryable<Supplier>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}