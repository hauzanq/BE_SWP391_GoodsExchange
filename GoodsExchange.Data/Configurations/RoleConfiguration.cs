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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r=>r.RoleId);

            builder.Property(p => p.RoleName).HasMaxLength(100).IsRequired();

            builder.HasOne(r=>r.User).WithMany(u=>u.Roles).HasForeignKey(u=>u.RoleId);
        }
    }
}
