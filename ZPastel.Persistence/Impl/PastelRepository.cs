using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZPastel.Core.Repositories;
using ZPastel.Model;

namespace ZPastel.Persistence.Impl
{
    public class PastelRepository : IPastelRepository
    {
        private readonly DataContext dataContext;

        public PastelRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IReadOnlyList<Pastel>> Filter(PastelFilter pastelFilter)
        {
            if (string.IsNullOrEmpty(pastelFilter.Name))
            {
                return new List<Pastel>();
            }

            var queryResults = dataContext
                .Set<Pastel>()
                .Where(p => p.IsAvailable)
                .AsQueryable();

            queryResults = queryResults.Where(q => EF.Functions.Like(q.Name, $"%{pastelFilter.Name}%"));

            return await queryResults.ToListAsync();
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
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
