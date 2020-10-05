using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZPastel.Persistence
{
    public interface IDataContext
    {
        void AddRange<T>(IEnumerable<T> instances) where T : class;

        Task<int> SaveChanges(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default);
    }
}
