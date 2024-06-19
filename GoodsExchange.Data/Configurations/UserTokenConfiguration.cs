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
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>

    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(ut => ut.Id);

            builder.Property(ut => ut.Token)
                   .IsRequired();

            builder.Property(ut => ut.ExpireDate)
                   .IsRequired();

            builder.HasOne<User>()
                   .WithMany(u => u.usertokens)
                   .HasForeignKey(ut => ut.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Configure the delete behavior as needed
        }
    }
}
