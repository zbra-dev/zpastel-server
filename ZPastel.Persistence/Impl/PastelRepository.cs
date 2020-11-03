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

        public async Task<Page<Pastel>> Filter(PastelFilter pastelFilter)
        {
            var queryResults = dataContext.Set<Pastel>().AsQueryable();
            queryResults = queryResults.Where(q => EF.Functions.Like(q.Name, $"%{pastelFilter.Name}%"));

            var pasteisFound = await queryResults
                .OrderByDescending(q => q.Id)
                .Where(e => e.IsAvailable)
                .Skip(pastelFilter.Skip)
                .Take(pastelFilter.Take + 1)
                .ToListAsync();

            var items = pasteisFound.Take(pastelFilter.Take).ToList();
            var hasMore = pasteisFound.Count > pastelFilter.Take;

            return new Page<Pastel>(items, hasMore);
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
