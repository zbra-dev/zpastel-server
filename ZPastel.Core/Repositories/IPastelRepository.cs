using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.API.Repositories
{
    public interface IPastelRepository
    {
        Task<IReadOnlyList<Pastel>> FindAll();
        Task<Pastel> FindById(long id);
    }
}
