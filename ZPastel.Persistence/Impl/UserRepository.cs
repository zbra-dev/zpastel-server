using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Service.API.Repositories;

namespace ZPastel.Persistence.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<User> FindById(long userId)
        {
            return await dataContext
                .Set<User>()
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == userId);
        }
    }
}
