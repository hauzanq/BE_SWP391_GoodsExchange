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
    public class RatingConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.ToTable("Ratings");

            builder.HasKey(r => r.RatingId);

            builder.Property(r => r.NumberStars).IsRequired();

            builder.Property(r => r.Feedback).HasMaxLength(255);

            builder.HasOne(u=>u.RatingGiven).WithMany(r=>r.RatingsGiven).HasForeignKey(u => u.RatingUserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u=>u.RatingReceived).WithMany(r=>r.RatingsReceived).HasForeignKey(u => u.TargetUserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Product).WithOne(p => p.Rate).HasForeignKey<Rate>(r =>r.ProductId);
        }
    }
}
