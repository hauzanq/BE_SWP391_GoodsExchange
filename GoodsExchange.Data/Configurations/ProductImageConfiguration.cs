using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsExchange.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImage");

            builder.HasKey(pi => pi.Id);

            builder.Property(pi => pi.ImagePath).HasMaxLength(200).IsRequired();

            builder.Property(pi => pi.Caption).HasMaxLength(200);

            builder.HasOne(pi=>pi.Product).WithMany(p=>p.ProductImages).HasForeignKey(pi => pi.ProductId);
        }
    }
}
