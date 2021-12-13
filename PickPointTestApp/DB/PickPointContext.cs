using Microsoft.EntityFrameworkCore;
using PickPointTestApp.DB.Models;
using System;
using Microsoft.Extensions.Configuration;

namespace PickPointTestApp.DB
{
    public class PickPointContext : DbContext
    {
        public PickPointContext(DbContextOptions<PickPointContext> options)
    : base(options)
        { }


        public PickPointContext()
    : base()
        { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Postomat> Postomats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PickPointDatabase"));
        }
    }
}
