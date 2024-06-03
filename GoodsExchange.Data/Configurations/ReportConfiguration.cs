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

            builder.Property(r => r.IsApprove).IsRequired().HasDefaultValue(false);

            builder.Property(r => r.IsActive).IsRequired().HasDefaultValue(true);

            builder.HasOne(u => u.ReportMade).WithMany(r => r.ReportsMade).HasForeignKey(u => u.ReportingUserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.ReportReceived).WithMany(r => r.ReportsReceived).HasForeignKey(u => u.TargetUserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Product).WithMany(p => p.Reports).HasForeignKey(r => r.ProductId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
