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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories"); 

            builder.HasKey(c => c.CategoryId); 

            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(255); 

            builder.HasMany(c=>c.Products).WithOne(p=>p.Category).HasForeignKey(p=>p.CategoryId);
        }
    }
}
