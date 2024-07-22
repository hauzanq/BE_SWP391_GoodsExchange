using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsExchange.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserName).IsRequired().HasMaxLength(255);

            builder.Property(u => u.UserImageUrl).HasMaxLength(255);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);

            builder.Property(u => u.Password).IsRequired().HasMaxLength(255);

            builder.Property(u => u.PhoneNumber).HasMaxLength(20);

            builder.Property(u => u.IsActive).HasDefaultValue(true);

            builder.HasMany(u => u.Products).WithOne(p => p.UserUpload).HasForeignKey(p => p.UserUploadId);

            builder.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
        }
    }
}
