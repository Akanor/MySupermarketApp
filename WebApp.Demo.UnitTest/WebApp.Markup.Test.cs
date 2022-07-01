using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Linq;
using UseCases.UseCaseInterfaces;
using WebApp.Pages;

namespace WebApp.Demo.UnitTest
{
    public class Tests 
    {
        private Bunit.TestContext testContext;
        private Mock<IViewCategoriesUseCase> viewMock;
        private Mock<IDeleteCategoryUseCase> delMock;

        private Mock<IViewProductsUseCase> viewProdMock;
        private Mock<IDeleteProductUseCase> delProdMock;
        private Mock<IGetCategoryByIdUseCase> getByIdMock;

        [SetUp]
        public void Setup()
        {
            testContext = new Bunit.TestContext();

            delMock = new Mock<IDeleteCategoryUseCase>();
            viewMock = new Mock<IViewCategoriesUseCase>();
            getByIdMock = new Mock<IGetCategoryByIdUseCase>();
            viewProdMock = new Mock<IViewProductsUseCase>();
            delProdMock = new Mock<IDeleteProductUseCase>();
        }

        [TearDown]
        public void TearDown()
        {
            testContext.Dispose();
        }

        [Test]
        public void Index_Markup_Test()
        {
            var component = testContext.RenderComponent<Index>();
            Assert.IsNotNull(component);
            Assert.IsTrue(component.Markup.Contains("<h1>Market-managment System</h1>"));
        }

        [Test]
        public void Categories_Markup_Test()
        {
            testContext.Services.AddTransient(x => delMock.Object);
            testContext.Services.AddTransient(x => viewMock.Object);           

            var component = testContext.RenderComponent<CategoriesComponent>();
            var buttons = component.FindAll("button");
            var submit = buttons.FirstOrDefault(b => b.OuterHtml.Contains("Add Category"));

            Assert.IsNotNull(component);
            Assert.IsTrue(component.Markup.Contains("<th>Name</th>"));
            Assert.IsTrue(component.Markup.Contains("<th>Description</th>"));
            Assert.IsNotNull(submit);
        }

        [Test]
        public void Products_Markup_Test()
        {
            testContext.Services.AddTransient(x => viewProdMock.Object);
            testContext.Services.AddTransient(x => delProdMock.Object);
            testContext.Services.AddTransient(x => getByIdMock.Object);

            var component = testContext.RenderComponent<ProductsComponent>();

            var buttons = component.FindAll("button");
            var submit = buttons.FirstOrDefault(b => b.OuterHtml.Contains("Add Product"));
           
            Assert.IsNotNull(component);
            Assert.IsTrue(component.Markup.Contains("<th>Product Category</th>"));
            Assert.IsTrue(component.Markup.Contains("<th>Product Name</th>"));
            Assert.IsTrue(component.Markup.Contains("<th>Price</th>"));
            Assert.IsTrue(component.Markup.Contains("<th>Quantity</th>"));
            Assert.IsNotNull(submit);
        }

    }
}