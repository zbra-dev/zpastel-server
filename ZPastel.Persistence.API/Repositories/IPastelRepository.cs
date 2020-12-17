using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Persistence.API.Repositories
{
    public interface IPastelRepository
    {
        Task<IReadOnlyList<Pastel>> FindAll();
        Task<Pastel> FindById(long id);
    }
}
