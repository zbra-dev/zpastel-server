using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ZPastel.Core.Repositories;
using ZPastel.Model;

namespace ZPastel.Persistence.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<User> FindByFirebaseId(string fireBaseId)
        {
            return await dataContext
                .Set<User>()
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.FirebaseId == fireBaseId);
        }

        public async Task<User> FindById(long userId)
        {
            return await dataContext
                .Set<User>()
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> CreateUser(User user)
        {
            dataContext.Add(user);

            await dataContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            dataContext.Update(user);

            await dataContext.SaveChangesAsync();

            return user;
        }
    }
}
