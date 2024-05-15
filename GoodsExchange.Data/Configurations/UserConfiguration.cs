using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId).ValueGeneratedOnAdd();

            builder.Property(u => u.UserName).IsRequired().HasMaxLength(255);

            builder.Property(u => u.UserImageUrl).HasMaxLength(255);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);

            builder.Property(u => u.Password).IsRequired().HasMaxLength(255);

            builder.Property(u => u.PhoneNumber).HasMaxLength(20);

            builder.Property(u => u.Status).IsRequired();

            builder.HasMany(u=>u.Roles).WithOne(r=>r.User).HasForeignKey(u=>u.RoleId);
        }
    }
}
