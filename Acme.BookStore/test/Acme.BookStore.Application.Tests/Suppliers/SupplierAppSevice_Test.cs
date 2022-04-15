//using Shouldly;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Acme.BookStore.Suppliers
//{
//    [Collection(BookStoreTestConsts.CollectionDefinitionName)]
//    public class SupplierAppSevice_Test: BookStoreApplicationTestBase
//    {
//        private readonly ISupplierAppSevice _supplierAppSevice;

//        public SupplierAppSevice_Test()
//        {
//            _supplierAppSevice = GetRequiredService<ISupplierAppSevice>();
//        }

//        //[Fact]
//        //public async Task Should_Get_All_Authors_Without_Any_Filter()
//        //{
//        //    var result = await _supplierAppSevice.GetListAsync(new GetSupplierListDto());

//        //    result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
//        //    result.Items.ShouldContain(supplier => supplier.Name == "Thanh Trung");
           
//        //}

//        //[Fact]
//        //public async Task Should_Get_Filtered_Suppliers()
//        //{
//        //    var result = await _supplierAppSevice.GetListAsync(
//        //        new GetSupplierListDto { Filter = "Thanh" });

//        //    result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
//        //    result.Items.ShouldContain(supplier => supplier.Name == "Thanh Trung");
            
//        //}

//        [Fact]
//        public async Task Should_Create_A_New_Supplier()
//        {
//            var supplierDto = await _supplierAppSevice.CreateAsync(
//                new CreateSupplierDto
//                {
//                    Name = "Thanh Trung",
//                    Phone = 0364267605,
//                    Address = "20 duong ba trac"
//                }
//            );

//            supplierDto.Id.ShouldNotBe(Guid.Empty);
//            supplierDto.Name.ShouldBe("Thanh Trung");
//        }

//        //[Fact]
//        //public async Task Should_Not_Allow_To_Create_Duplicate_Supplier()
//        //{
//        //    await Assert.ThrowsAsync<SupplierAlreadyExistsException>(async () =>
//        //    {
//        //        await _supplierAppSevice.CreateAsync(
//        //            new CreateSupplierDto
//        //            {
//        //                Name = "Douglas Adams",
//        //                Phone = 0364267605,
//        //                Address = "..."
//        //            }
//        //        );
//        //    });
//        //}
//    }
//}
