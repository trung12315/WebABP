using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Suppliers;
using Acme.BookStore.Users;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Acme.BookStore.MongoDB;

[ConnectionStringName("Default")]
public class BookStoreMongoDbContext : AbpMongoDbContext
{
    public IMongoCollection<User> Users => Collection<User>();
    public IMongoCollection<Supplier> Suppliers => Collection<Supplier>(); 
    public IMongoCollection<Author> Authors => Collection<Author>();
    public IMongoCollection<Book> Books => Collection<Book>();
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        //builder.Entity<YourEntity>(b =>
        //{
        //    //...
        //});
    }
}
