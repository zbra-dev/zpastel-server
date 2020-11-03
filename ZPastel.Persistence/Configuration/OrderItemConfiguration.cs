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
            builder.Property(o => o.Name).HasColumnName("Name");
            builder.Property(o => o.Ingredients).HasColumnName("Ingredients");
            builder.Property(o => o.Price).HasColumnName("Price").HasColumnType("decimal(18,2)"); ;
            builder.Property(o => o.Quantity).HasColumnName("Quantity");
            builder.Property(o => o.CreatedOn).HasColumnName("CreatedOn");
            builder.Property(o => o.LastModifiedById).HasColumnName("LastModifiedById");
            builder.Property(o => o.LastModifiedOn).HasColumnName("LastModifiedOn");

            builder.HasOne(o => o.Order).WithMany().HasForeignKey(p => p.OrderId).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            builder.HasOne(o => o.Pastel).WithMany().HasForeignKey(p => p.PastelId).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            builder.HasOne(o => o.User).WithMany().HasForeignKey(p => p.CreatedById);
        }
    }
}
