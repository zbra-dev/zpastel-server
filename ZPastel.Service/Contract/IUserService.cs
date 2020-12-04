using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.Contract
{
    public interface IUserService
    {
        Task<User> FindByFirebaseId(string firebaseId);
        Task<User> FindById(long id);
        Task<User> Save(User user);
    }
}
