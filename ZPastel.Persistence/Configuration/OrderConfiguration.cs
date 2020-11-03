﻿using Microsoft.EntityFrameworkCore;
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
            builder.Property(o => o.TotalPrice).HasColumnName("TotalPrice").HasColumnType("decimal(18,2)");
            builder.Property(o => o.CreatedByUsername).HasColumnName("CreatedByUsername");
            builder.Property(o => o.CreatedById).HasColumnName("CreatedById");
            builder.Property(o => o.CreatedOn).HasColumnName("CreatedOn");
            builder.Property(o => o.LastModifiedById).HasColumnName("LastModifiedById");
            builder.Property(o => o.LastModifiedOn).HasColumnName("LastModifiedOn");

            builder.HasMany(o => o.OrderItems).WithOne().HasForeignKey(e => e.OrderItemId).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            builder.HasOne(o => o.User).WithMany().HasForeignKey(p => p.CreatedById);
        }
    }
}
