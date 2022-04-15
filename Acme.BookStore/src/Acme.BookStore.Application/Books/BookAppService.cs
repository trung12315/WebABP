using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Acme.BookStore.Suppliers;

namespace Acme.BookStore.Books
{
    [Authorize(BookStorePermissions.Books.Default)]
    public class BookAppService :
        CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
        IBookAppService //implement the IBookAppService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ISupplierRepository _supplierRepository;

        public BookAppService(
            IRepository<Book, Guid> repository, ISupplierRepository supplierRepository,
            IAuthorRepository authorRepository)
            : base(repository)
        {
            _supplierRepository= supplierRepository;
            _authorRepository = authorRepository;
            GetPolicyName = BookStorePermissions.Books.Default;
            GetListPolicyName = BookStorePermissions.Books.Default;
            CreatePolicyName = BookStorePermissions.Books.Create;
            UpdatePolicyName = BookStorePermissions.Books.Edit;
            DeletePolicyName = BookStorePermissions.Books.Create;
        }

        public async override Task<BookDto> GetAsync(Guid id)
        {
            var book = await Repository.GetAsync(id);
            var bookDto = ObjectMapper.Map<Book, BookDto>(book);
            var supplier = await _supplierRepository.GetAsync(book.SupplierId);
            bookDto.SupplierName = supplier.Name;
            var author = await _authorRepository.GetAsync(book.AuthorId);
            bookDto.AuthorName = author.Name;
            
            return bookDto;
        }

        public async override Task<PagedResultDto<BookDto>>
            GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Set a default sorting, if not provided
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Book.Name);
            }

            //Get the IQueryable<Book> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Get the books
            var books = await AsyncExecuter.ToListAsync(
                queryable
                    .OrderBy(input.Sorting)
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
            );

            //Convert to DTOs
            var bookDtos = ObjectMapper.Map<List<Book>, List<BookDto>>(books);

            //Get a lookup dictionary for the related authors
            var authorDictionary = await GetAuthorDictionaryAsync(books);

          
            //Set AuthorName for the DTOs
            bookDtos.ForEach(bookDto => bookDto.AuthorName =
                             authorDictionary[bookDto.AuthorId].Name);

            var supplierDictionary = await GetSupplierDictionaryAsync(books);

            bookDtos.ForEach(bookDto => bookDto.SupplierName =
                     supplierDictionary[bookDto.SupplierId].Name);

            
            //Get the total count with another query (required for the paging)
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<BookDto>(
                totalCount,
                bookDtos
            );
        }

        public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        {
            var authors = await _authorRepository.GetListAsync();

            return new ListResultDto<AuthorLookupDto>(
                ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
            );
        }
        public async Task<ListResultDto<SupplierLookupDto>> GetSupplierLookupAsync()
        {
            var suppliers = await _supplierRepository.GetListAsync();

            return new ListResultDto<SupplierLookupDto>(
                ObjectMapper.Map<List<Supplier>, List<SupplierLookupDto>>(suppliers)
            );
        }
        private async Task<Dictionary<Guid, Supplier>>
           GetSupplierDictionaryAsync(List<Book> books)
        {
            var SupplierIds = books
                .Select(b => b.SupplierId)
                .Distinct()
                .ToArray();

            var queryable = await _supplierRepository.GetQueryableAsync();

            var suppliers = await AsyncExecuter.ToListAsync(
                queryable.Where(a => SupplierIds.Contains(a.Id))
            );

            return suppliers.ToDictionary(x => x.Id, x => x);
        }
        private async Task<Dictionary<Guid, Author>>
            GetAuthorDictionaryAsync(List<Book> books)
        {
            var authorIds = books
                .Select(b => b.AuthorId)
                .Distinct()
                .ToArray();

            var queryable = await _authorRepository.GetQueryableAsync();

            var authors = await AsyncExecuter.ToListAsync(
                queryable.Where(a => authorIds.Contains(a.Id))
            );

            return authors.ToDictionary(x => x.Id, x => x);
        }
    }
}
