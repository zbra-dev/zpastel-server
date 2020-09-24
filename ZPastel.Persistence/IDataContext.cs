using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ZPastel.Persistence
{
    public interface IDataContext
    {
        IEnumerable<T> AddRange<T>(IEnumerable<T> instances) where T : class;

        Task<int> SaveChanges(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default);
    }
}
