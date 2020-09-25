using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZPastel.Model;

namespace ZPastel.Persistence.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasColumnName("Id");
            builder.Property(o => o.TotalPrice).HasColumnName("TotalPrice");
            builder.Property(o => o.Date).HasColumnName("Data");
            builder.HasMany(o => o.OrderItems).WithOne(o => o.Order);
        }
    }
}
