using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZPastel.Model;

namespace ZPastel.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirebaseId).HasColumnName("FirebaseId");
            builder.Property(u => u.Name).HasColumnName("Name");
            builder.Property(u => u.CreatedById).HasColumnName("CreatedById");
            builder.Property(u => u.CreatedOn).HasColumnName("CreatedOn");
            builder.Property(u => u.Email).HasColumnName("Email");
            builder.Property(u => u.LastModifiedById).HasColumnName("LastModifiedById");
            builder.Property(u => u.LastModifiedOn).HasColumnName("LastModifiedOn");
            builder.Property(u => u.PhotoUrl).HasColumnName("PhotoUrl");
        }
    }
}
