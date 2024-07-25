using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsExchange.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductName).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Description).HasMaxLength(500).IsRequired();

            builder.Property(p => p.Price).IsRequired();

            builder.Property(p => p.IsActive).IsRequired();

            builder.Property(p => p.UploadDate).HasColumnType("datetime2");

            builder.Property(p => p.IsApproved).HasDefaultValue(false);

            builder.Property(p => p.ApprovedDate).HasColumnType("datetime2");

            builder.Property(p => p.IsReviewed).HasDefaultValue(false);

            builder.HasMany(p => p.Reports).WithOne(r => r.Product).HasForeignKey(r => r.ProductId);

            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);

            builder.HasOne(p => p.UserUpload).WithMany(u => u.Products).HasForeignKey(p => p.UserUploadId);

            builder.HasMany(p => p.ProductImages).WithOne(pi => pi.Product).HasForeignKey(p => p.ProductId);
        }
    }
}
