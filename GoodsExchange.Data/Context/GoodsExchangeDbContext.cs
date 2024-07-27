using GoodsExchange.Data.Configurations;
using GoodsExchange.Data.Extensions;
using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GoodsExchange.Data.Context
{
    public class GoodsExchangeDbContext : DbContext
    {
        public GoodsExchangeDbContext()
        {

        }
        public GoodsExchangeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ExchangeRequest> ExchangeRequests { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                string file = environment == "Production" ? "appsettings.Production.json" : "appsettings.Development.json";
                string root = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
                string apiDirectory = Path.Combine(root, "GoodsExchange.API");
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(apiDirectory)
                    .AddJsonFile(file)
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new RatingConfiguration());
            modelBuilder.ApplyConfiguration(new ExchangeRequestConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.Seed();
        }
    }
}
