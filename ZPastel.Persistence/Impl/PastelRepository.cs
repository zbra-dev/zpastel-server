using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Service.Repositories;

namespace ZPastel.Persistence.Impl
{
    public class PastelRepository : IPastelRepository
    {
        private readonly DataContext dataContext;

        public PastelRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IReadOnlyList<Pastel>> FindAll()
        {
            return await dataContext
                .Set<Pastel>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Pastel> FindById(long id)
        {
            return await dataContext
                .Set<Pastel>()
                .Where(p => p.Id == id)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
    }
}
