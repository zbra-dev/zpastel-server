using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindById(long userId);
    }
}
