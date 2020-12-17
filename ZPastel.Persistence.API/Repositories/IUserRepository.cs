using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Persistence.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindById(long userId);
    }
}
