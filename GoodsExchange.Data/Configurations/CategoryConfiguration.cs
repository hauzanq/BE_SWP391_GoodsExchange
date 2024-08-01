using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsExchange.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(255);

            builder.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

            builder.Property(c => c.DateCreated).HasDefaultValueSql("GETUTCDATE()").HasColumnType("datetime2"); ;
        }
    }
}
