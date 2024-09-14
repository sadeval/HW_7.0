using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace ShopDB
{
    public class ShopContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ShopDB;Trusted_Connection=True;");
        }
    }

}
