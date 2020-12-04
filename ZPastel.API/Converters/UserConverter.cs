using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class UserConverter
    {
        public User ConvertToModel(UserResource userResource)
        {
            return new User
            {
                CreatedById = userResource.CreatedById,
                CreatedOn = userResource.CreatedOn,
                Email = userResource.Email,
                Id = userResource.Id,
                FirebaseId = userResource.FirebaseId,
                LastModifiedById = userResource.LastModifiedById,
                LastModifiedOn = userResource.LastModifiedOn,
                Name = userResource.Name,
                PhotoUrl = userResource.PhotoUrl
            };
        }

        public UserResource ConvertToResource(User user)
        {
            return new UserResource
            {
                CreatedById = user.CreatedById,
                CreatedOn = user.CreatedOn,
                Email = user.Email,
                Id = user.Id,
                FirebaseId = user.FirebaseId,
                LastModifiedById = user.LastModifiedById,
                LastModifiedOn = user.LastModifiedOn,
                Name = user.Name,
                PhotoUrl = user.PhotoUrl
            };
        }
    }
}
