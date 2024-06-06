using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsExchange.Data.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");

            builder.HasKey(r => r.ReportId);

            builder.Property(r => r.Reason).IsRequired().HasMaxLength(255);

            builder.Property(r => r.CreateDate).HasColumnType("datetime");

            builder.Property(r => r.IsApprove).IsRequired();

            builder.Property(r => r.IsActive).IsRequired();

            builder.HasOne(u => u.Sender).WithMany(r => r.ReportsMade).HasForeignKey(u => u.SenderId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.Receiver).WithMany(r => r.ReportsReceived).HasForeignKey(u => u.ReceiverId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Product).WithMany(p => p.Reports).HasForeignKey(r => r.ProductId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
