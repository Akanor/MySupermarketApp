using System;
using Xunit;


using Microsoft.Extensions.DependencyInjection;
using Plugins.DataStore.SQL;
using System.Linq;
using UseCases.ProductsUseCases;
using UseCases.UseCaseInterfaces;
using WebApp.Pages;
using Microsoft.EntityFrameworkCore;
using UseCases.CategoriesUseCases;
using CoreBusiness;

namespace WebApp.Demo.Data.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Category_DbContext_Test()
        {
            var options = new DbContextOptionsBuilder<MarketContext>()
                .UseInMemoryDatabase(databaseName: "MarketManagment")
                .Options;

            var context = new MarketContext(options);
            var prodRepo = new CategoryRepository(context);
            var useCase = new ViewCategoriesUseCase(prodRepo);

            context.Categories.AddRange(
              Enumerable.Range(1, 5).Select(p => new Category { Name = "name" })
              );

            context.SaveChanges();

            var objList = useCase.Execute();

            Assert.NotNull(objList);
            Assert.Equal(5, objList.Count());
        }

        [Fact]
        public void Product_DbContext_Test()
        {
            var options = new DbContextOptionsBuilder<MarketContext>()
                .UseInMemoryDatabase(databaseName: "MarketManagment")
                .Options;

            var context = new MarketContext(options);
            var prodRepo = new ProductRepository(context);
            var useCase = new ViewProductsUseCase(prodRepo);

            context.Products.AddRange(
              Enumerable.Range(1,10).Select( p=> new Product { CategoryId = 1, Name = "name", Price = 25, Quantity = 100 })
              );

            context.SaveChanges();

            var objList = useCase.Execute();

            Assert.NotNull(objList);
            Assert.Equal(10, objList.Count());
        }

        
    }
}
