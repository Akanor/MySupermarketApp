using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using System;

namespace Plugins.DataStore.SQL
{
    public class MarketContext : DbContext
    {
        public MarketContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            //seed data
            modelBuilder.Entity<Category>().HasData(
                new Category() { CategoryId = 1, Name = "Drinks", Description = "Drinks" },
                new Category() { CategoryId = 2, Name = "Milk-products", Description = "Milk-products" },
                new Category() { CategoryId = 3, Name = "Meat", Description = "Meat" }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product() { ProductId = 1, CategoryId = 1, Name = "Green Tea", Quantity = 100, Price = 25 },
                new Product() { ProductId = 2, CategoryId = 2, Name = "Cheese", Quantity = 100, Price = 25 },
                new Product() { ProductId = 3, CategoryId = 3, Name = "Beaf", Quantity = 100, Price = 25 },
                new Product() { ProductId = 4, CategoryId = 3, Name = "Chicken", Quantity = 100, Price = 25 }
                );
        }
    }
}
