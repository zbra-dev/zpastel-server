using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZPastel.Persistence.Model;

namespace ZPastel.Persistence.Configuration
{
    public class PastelEntityConfiguration : IEntityTypeConfiguration<PastelEntity>
    {
        public void Configure(EntityTypeBuilder<PastelEntity> builder)
        {
            throw new System.NotImplementedException();
        }
    }
}
