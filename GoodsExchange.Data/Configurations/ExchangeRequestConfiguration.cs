using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsExchange.Data.Configurations
{
    public class ExchangeRequestConfiguration : IEntityTypeConfiguration<ExchangeRequest>
    {
        public void Configure(EntityTypeBuilder<ExchangeRequest> builder)
        {
            builder.HasKey(po => po.ExchangeRequestId);

            builder.HasOne(po => po.Receiver).WithMany(u => u.PreOrderToSellers).HasForeignKey(po => po.ReceiverId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(po => po.Sender).WithMany(u => u.PreOrderToBuyers).HasForeignKey(po => po.SenderId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(po => po.CurrentProduct).WithMany(p => p.ExchangeRequestsSent).HasForeignKey(po => po.CurrentProductId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(po => po.TargetProduct).WithMany(p => p.ExchangeRequestsReceived).HasForeignKey(po => po.TargetProductId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(po => po.SenderStatus).HasDefaultValue(false);

            builder.Property(po => po.ReceiverStatus).HasDefaultValue(false);

            builder.Property(po => po.StartTime).HasDefaultValueSql("GETUTCDATE()").HasColumnType("datetime2");

            builder.Property(po => po.EndTime).HasColumnType("datetime2");
        }
    }
}
