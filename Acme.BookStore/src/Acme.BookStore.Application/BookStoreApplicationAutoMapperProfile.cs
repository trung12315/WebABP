using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Suppliers;
using Acme.BookStore.Users;
using AutoMapper;

namespace Acme.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
       
        CreateMap<Supplier, SupplierDto>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<Author, AuthorLookupDto>();
        CreateMap<User, UserDto>();
        CreateMap<Supplier, SupplierLookupDto>();
        //CreateMap<Supplier, SupplierLookupDto>();

        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
