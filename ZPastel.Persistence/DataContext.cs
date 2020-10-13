using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ZPastel.Persistence.Configuration;

namespace ZPastel.Persistence
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new PastelConfiguration());
            modelBuilder.ApplyConfiguration(new PasteleiroConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public void AddRange<T>(IEnumerable<T> instances) where T : class
        {
            base.AddRange(instances);
        }

        public Task<int> SaveChanges(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default)
        {
           return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
