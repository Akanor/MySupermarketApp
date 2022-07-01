using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class ProductInMemoryRepository : IProductRepository
    {
        private List<Product> products;

        public ProductInMemoryRepository()
        {
            products = new List<Product>()
            {
                new Product() { ProductId = 1, CategoryId = 1, Name ="Iced Tea", Quantity = 100, Price = 25 },
                new Product() { ProductId = 2, CategoryId = 2, Name ="Ginger Tea", Quantity = 100, Price = 25 },
                new Product() { ProductId = 3, CategoryId = 3, Name ="Whole bread", Quantity = 100, Price = 25 }
            };
        }

        public void AddProduct(Product product)
        {
            if (products.Any(x => x.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))) return;

            if (products != null && products.Count > 0)
            {
                var maxId = products.Max(x => x.CategoryId);
                product.ProductId = (int)(maxId + 1);
            }
            else
            {
                product.ProductId = 1;
            }
            products.Add(product);
        }

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public Product GetProductById(int productId)
        {
            return products.FirstOrDefault(x => x.ProductId == productId);  
        }

        public void UpdateProduct(Product product)
        {
            var productToUpdate = GetProductById(product.ProductId);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.Price = product.Price;
                productToUpdate.Quantity = product.Quantity;
            }
        }

        public void DeleteProduct(int productId)
        {
            products?.Remove(GetProductById(productId));
            //var productToDelete = GetProductById(productId);
            //if(productToDelete != null) products?.Remove(productToDelete);
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return products.Where(x => x.CategoryId == categoryId);
        }
    }
}
