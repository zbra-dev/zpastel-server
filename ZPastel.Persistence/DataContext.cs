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
            modelBuilder.ApplyConfiguration(new PastelConfiguration());
        }

        public IEnumerable<T> AddRange<T>(IEnumerable<T> instances) where T : class
        {
            return new List<T>();
        }

        public Task<int> SaveChanges(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(1);
        }
    }
}
