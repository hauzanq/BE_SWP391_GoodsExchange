using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsExchange.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => r.RatingId);

            builder.Property(r => r.NumberStars).IsRequired();

            builder.Property(r => r.Feedback).HasMaxLength(255);

            builder.Property(r => r.DateCreated).HasColumnType("datetime2");

            builder.HasOne(u => u.Sender).WithMany(r => r.RatingsGiven).HasForeignKey(u => u.SenderId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.Receiver).WithMany(r => r.RatingsReceived).HasForeignKey(u => u.ReceiverId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Product).WithOne(p => p.Rate).HasForeignKey<Rating>(r => r.ProductId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
