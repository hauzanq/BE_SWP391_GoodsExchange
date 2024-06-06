using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductName).HasMaxLength(100).IsRequired();

            builder.Property(p => p.Description).HasMaxLength(500).IsRequired();

            builder.Property(p => p.ProductImageUrl).HasMaxLength(500).IsRequired();

            builder.Property(p => p.Price).IsRequired();

            builder.Property(p => p.IsActive).IsRequired();


            builder.HasMany(p => p.Reports).WithOne(r => r.Product).HasForeignKey(r => r.ProductId);

            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);

            builder.HasOne(p => p.UserUpload).WithMany(u => u.Products).HasForeignKey(p => p.UserUploadId);
        }
    }
}
