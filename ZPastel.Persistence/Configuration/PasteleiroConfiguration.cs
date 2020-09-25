using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZPastel.Model;

namespace ZPastel.Persistence.Configuration
{
    public class PasteleiroConfiguration : IEntityTypeConfiguration<Pasteleiro>
    {
        public void Configure(EntityTypeBuilder<Pasteleiro> builder)
        {
            builder.ToTable("Pasteleiro");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Name).HasColumnName("Name");
        }
    }
}
