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
        public void CategoriesUseCases_Test()
        {
            var options = new DbContextOptionsBuilder<MarketContext>()
                .UseInMemoryDatabase(databaseName: "MarketManagment")
                .Options;

            var context = new MarketContext(options);
            var catRepo = new CategoryRepository(context);
            var viewUseCase = new ViewCategoriesUseCase(catRepo);
            var addCategoryUseCase = new AddCategoryUseCase(catRepo);
            var deleteCategoryUseCase = new DeleteCategoryUseCase(catRepo);

            context.Categories.AddRange(
              Enumerable.Range(1, 5).Select(p => new Category { Name = "name" })
              );
            context.SaveChanges();

            var objList = viewUseCase.Execute();
            Assert.NotNull(objList);
            Assert.Equal(5, objList.Count());


            addCategoryUseCase.Execute(new Category { Name = "name" });
            var newList = viewUseCase.Execute();
            Assert.NotNull(newList);
            Assert.Equal(6, newList.Count());

            deleteCategoryUseCase.Delete(1);
            deleteCategoryUseCase.Delete(2);

            var newList2 = viewUseCase.Execute();
            Assert.NotNull(newList2);
            Assert.Equal(4, newList2.Count());

        }

      
        [Fact]
        public void ViewProducts_Test()
        {
            var options = new DbContextOptionsBuilder<MarketContext>()
                .UseInMemoryDatabase(databaseName: "MarketManagment")
                .Options;

            var context = new MarketContext(options);
            var prodRepo = new ProductRepository(context);
            var ViewProductUseCase = new ViewProductsUseCase(prodRepo);
            var addProductUseCase = new AddProductUseCase(prodRepo);
            var deleteProductUseCase = new DeleteProductUseCase(prodRepo);

            context.Products.AddRange(
              Enumerable.Range(1,10).Select( p=> new Product { CategoryId = 1, Name = "name", Price = 25, Quantity = 100 })
              );

            context.SaveChanges();

            var objList = ViewProductUseCase.Execute();

            Assert.NotNull(objList);
            Assert.Equal(10, objList.Count());


            for(int i = 0; i < 5; i++)
                addProductUseCase.Execute(new Product { CategoryId = 1, Name = "name", Price = 25, Quantity = 100 });

            var objList2 = ViewProductUseCase.Execute();

            Assert.NotNull(objList2);
            Assert.Equal(15, objList2.Count());

            for (int i = 1; i < 8; i++)
                deleteProductUseCase.Execute(i);

            Assert.NotNull(ViewProductUseCase.Execute());
            Assert.Equal(8, ViewProductUseCase.Execute().Count());
        }

        
    }
}
