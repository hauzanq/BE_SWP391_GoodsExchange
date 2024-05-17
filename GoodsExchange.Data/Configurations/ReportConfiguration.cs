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
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");

            builder.HasKey(r => r.ReportId);

            builder.Property(r => r.ReportId).ValueGeneratedOnAdd();

            builder.Property(r => r.Reason).IsRequired().HasMaxLength(255);

            builder.HasOne(u => u.ReportMade).WithMany(r => r.ReportsMade).HasForeignKey(u => u.ReportingUserId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(u => u.ReportReceived).WithMany(r => r.ReportsReceived).HasForeignKey(u => u.TargetUserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
