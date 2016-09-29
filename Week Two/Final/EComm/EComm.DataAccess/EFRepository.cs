using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EComm.Model;

namespace EComm.DataAccess
{
    public static class EFRepositoryFactory
    {
        public static IRepository GetInstance(string connStr)
        {
            return new EFRepository(connStr);
        }
    }

    internal class EFRepository : DbContext, IRepository
    {
        private string _connStr;

        public EFRepository(string connStr)
        {
            _connStr = connStr;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_connStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Supplier>().ToTable("Supplier");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public IEnumerable<Product> GetAllProducts()
        {
            return Products;
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return Suppliers;
        }

        public void SaveProduct(Product product)
        {
            this.Update(product);
            this.SaveChanges();

            //var prod = Products.SingleOrDefault(p => p.Id == product.Id);
            //prod.ProductName = product.ProductName;
            //prod.UnitPrice = product.UnitPrice;
            //this.SaveChanges();
        }
    }
}
