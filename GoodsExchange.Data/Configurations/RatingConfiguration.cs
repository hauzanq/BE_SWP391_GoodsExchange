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
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.ToTable("Ratings");

            builder.HasKey(r => r.RatingId);

            builder.Property(r => r.NumberStars).IsRequired();

            builder.Property(r => r.Feedback).HasMaxLength(255);

            builder.Property(r => r.CreateDate).HasColumnType("datetime");

            builder.HasOne(u=>u.Sender).WithMany(r=>r.RatingsGiven).HasForeignKey(u => u.SenderId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u=>u.Receiver).WithMany(r=>r.RatingsReceived).HasForeignKey(u => u.ReceiverId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Product).WithOne(p => p.Rate).HasForeignKey<Rating>(r =>r.ProductId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
