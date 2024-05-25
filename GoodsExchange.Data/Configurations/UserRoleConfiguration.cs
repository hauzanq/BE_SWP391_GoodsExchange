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
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(ur => new {ur.UserId , ur.RoleId});

            builder.HasOne(ur=> ur.User).WithMany(u=>u.UserRoles).HasForeignKey(u=>u.UserId);
            builder.HasOne(ur=> ur.Role).WithMany(u=>u.UserRoles).HasForeignKey(u=>u.RoleId);
        }
    }
}
