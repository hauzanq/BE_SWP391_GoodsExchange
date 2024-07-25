//using GoodsExchange.Data.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace GoodsExchange.Data.Configurations
//{
//    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
//    {
//        public void Configure(EntityTypeBuilder<Transaction> builder)
//        {
//            builder.HasKey(t => t.TransactionId);

//            builder.HasOne(t => t.PreOrder).WithOne(p => p.Transaction).HasForeignKey<Transaction>(t => t.PreOrderId).OnDelete(DeleteBehavior.NoAction);
//        }
//    }
//}
