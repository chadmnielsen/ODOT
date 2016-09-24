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

    public class EFRepository : DbContext, IRepository
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
        }

        public DbSet<Product> Products { get; set; }

        public IEnumerable<Product> GetAllProducts()
        {
            return Products;
        }

        void IRepository.SaveChanges()
        {
            this.SaveChanges();
        }
    }
}
