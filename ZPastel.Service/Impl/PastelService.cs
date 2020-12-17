using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Persistence.API.Repositories;
using ZPastel.Service.API.Contract;

namespace ZPastel.Service.Impl
{
    public class PastelService : IPastelService
    {
        private readonly IPastelRepository pastelRepository;

        public PastelService(IPastelRepository pastelRepository)
        {
            this.pastelRepository = pastelRepository;
        }

        public async Task<IReadOnlyList<Pastel>> Filter(PastelFilter pastelFilter)
        {
            return await pastelRepository.Filter(pastelFilter);
        }

        public async Task<IReadOnlyList<Pastel>> FindAll()
        {
            return await pastelRepository.FindAll();
        }
    }
}
