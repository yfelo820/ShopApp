using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DB_Context
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options):base(options) 
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .Property(p => p.Price)
                        .HasPrecision(7, 2);

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Product 1", Description = "Description 1", Price = 10.59m, Stock = 100 },
                new Product { ProductId = 2, Name = "Product 2", Description = "Description 2", Price = 20.50m, Stock = 200 },
                new Product { ProductId = 3, Name = "Product 3", Description = "Description 3", Price = 33.00m, Stock = 50 },
                new Product { ProductId = 4, Name = "Product 4", Description = "Description 4", Price = 110.20m, Stock = 40 });

            modelBuilder.Entity<Client>().HasData(
                new Client { ClientId = 1, Name = "Client 1", Email = "client1@gmail.com", PhoneNumber = "54152905" },
                new Client { ClientId = 2, Name = "Client 2", Email = "client2@gmail.com", PhoneNumber = "59082906" },
                new Client { ClientId = 3, Name = "Client 3", Email = "client3@gmail.com", PhoneNumber = "54562907" },
                new Client { ClientId = 4, Name = "Client 4", Email = "client4@gmail.com", PhoneNumber = "51232908" });
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sales> Sales { get; set; }
    }
}
