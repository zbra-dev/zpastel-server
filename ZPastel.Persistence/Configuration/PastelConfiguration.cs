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
            builder.Property(p => p.Ingredients).HasColumnName("Ingredients");
            builder.Property(p => p.IsAvailable).HasColumnName("IsAvailable");
            builder.Property(p => p.Price).HasColumnName("Price");
            builder.HasOne(p => p.Pasteleiro).WithMany().HasForeignKey(p => p.PasteleiroId);
        }
    }
}
