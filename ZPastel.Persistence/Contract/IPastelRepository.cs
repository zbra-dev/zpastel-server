using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Persistence.Contract
{
    public interface IPastelRepository
    {
        Task<IReadOnlyList<Pastel>> FindAll();
    }
}
