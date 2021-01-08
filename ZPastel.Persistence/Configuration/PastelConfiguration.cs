using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZPastel.Model;

namespace ZPastel.Persistence.Configuration
{
    public class PastelConfiguration : IEntityTypeConfiguration<Pastel>
    {
        public void Configure(EntityTypeBuilder<Pastel> builder)
        {
            builder.ToTable("Pastel");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Name).HasColumnName("Name");
            builder.Property(p => p.Ingredients).HasColumnName("Ingredients");
            builder.Property(p => p.IsAvailable).HasColumnName("IsAvailable");
            builder.Property(p => p.Price).HasColumnName("Price").HasColumnType("decimal(18,2)");
            builder.Property(o => o.CreatedById).HasColumnName("CreatedById");
            builder.Property(o => o.CreatedOn).HasColumnName("CreatedOn");
            builder.Property(o => o.LastModifiedById).HasColumnName("LastModifiedById");
            builder.Property(o => o.LastModifiedOn).HasColumnName("LastModifiedOn");
            builder.Property(p => p.FlavorImageUrl).HasColumnName("FlavorImageUrl");

            builder.HasOne(p => p.Pasteleiro).WithMany().HasForeignKey(p => p.PasteleiroId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.CreatedById).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
