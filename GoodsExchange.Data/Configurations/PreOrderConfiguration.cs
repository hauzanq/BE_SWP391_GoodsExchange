using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodsExchange.Data.Configurations
{
    public class PreOrderConfiguration : IEntityTypeConfiguration<PreOrder>
    {
        public void Configure(EntityTypeBuilder<PreOrder> builder)
        {
            builder.HasKey(po => po.PreOrderId);

            builder.HasOne(po => po.Seller).WithMany(u => u.PreOrderToSellers).HasForeignKey(po => po.SellerId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(po => po.Buyer).WithMany(u => u.PreOrderToBuyers).HasForeignKey(po => po.BuyerId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(po => po.CurrentProduct).WithMany(p => p.ExchangeRequestsSent).HasForeignKey(po => po.CurrentProductId).OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(po => po.TargetProduct).WithMany(p => p.ExchangeRequestsReceived).HasForeignKey(po => po.TargetProductId).OnDelete(DeleteBehavior.NoAction);

            builder.Property(po => po.BuyerConfirmed).HasDefaultValue(false);

            builder.Property(po => po.SellerConfirmed).HasDefaultValue(false);

            builder.Property(po => po.DateCreated).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
