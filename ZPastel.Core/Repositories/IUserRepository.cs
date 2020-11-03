using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindById(long userId);
    }
}
