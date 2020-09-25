using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZPastel.Model;

namespace ZPastel.Persistence.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasColumnName("Id");
            builder.Property(o => o.Ingredients).HasColumnName("Ingredients");
            builder.Property(o => o.OrderId).HasColumnName("OrderId");
            builder.Property(o => o.PastelId).HasColumnName("PastelId");
            builder.Property(o => o.Price).HasColumnName("Price");
            builder.Property(o => o.Quantity).HasColumnName("Quantity");
            builder.Property(o => o.Date).HasColumnName("Date");
            builder.HasOne(o => o.Order).WithOne().HasForeignKey<OrderItem>(p => p.OrderId);
            builder.HasOne(o => o.Pastel).WithOne().HasForeignKey<OrderItem>(p => p.PastelId);
        }
    }
}
