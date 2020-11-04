using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.Contract
{
    public interface IPastelService
    {
        Task<IReadOnlyList<Pastel>> FindAll();
        Task<IReadOnlyList<Pastel>> Filter(PastelFilter pastelFilter);
    }
}
