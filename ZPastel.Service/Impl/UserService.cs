using System.Threading.Tasks;
using ZPastel.Core.Repositories;
using ZPastel.Model;
using ZPastel.Service.Contract;
using ZPastel.Service.Exceptions;
using ZPastel.Service.Validators;

namespace ZPastel.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly UserValidator userValidator;

        public UserService(IUserRepository userRepository, UserValidator userValidator)
        {
            this.userRepository = userRepository;
            this.userValidator = userValidator;
        }

        public async Task<User> FindByFirebaseId(string firebaseId)
        {
            var user = await userRepository.FindByFirebaseId(firebaseId);

            return user;
        }

        public async Task<User> FindById(long id)
        {
            var user = await userRepository.FindById(id);

            if (user == null)
            {
                throw new NotFoundException<User>(id.ToString(), nameof(User.Id));
            }

            return user;
        }

        public async Task<User> Save(User user)
        {
            userValidator.Validate(user);

            var foundUser = await userRepository.FindByFirebaseId(user.FirebaseId);

            if (foundUser != null)
            {
                foundUser.Email = user.Email;
                foundUser.LastModifiedById = user.LastModifiedById;
                foundUser.LastModifiedOn = user.LastModifiedOn;
                foundUser.Name = user.Name;
                foundUser.PhotoUrl = user.PhotoUrl;

                return await userRepository.UpdateUser(user);
            }

            return await userRepository.CreateUser(user);
        }
    }
}
