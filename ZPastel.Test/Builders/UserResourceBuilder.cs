using System;
using ZPastel.API.Resources;

namespace ZPastel.Test.Builders
{
    public class UserResourceBuilder
    {
        private readonly UserResource userResource;

        public UserResourceBuilder()
        {
            userResource = new UserResource();
        }

        public UserResourceBuilder WithDefaultValues()
        {
            var now = DateTime.Now;
            userResource.CreatedOn = now;
            userResource.LastModifiedOn = now;
            userResource.CreatedById = 1;
            userResource.LastModifiedById = 1;
            userResource.Email = "default@test.com";
            userResource.FirebaseId = "default-id1";
            userResource.Name = "Default user";
            userResource.PhotoUrl = "www.photos.test.com/default.jpg";

            return this;
        }

        public UserResourceBuilder WithEmail(string email)
        {
            userResource.Email = email;

            return this;
        }

        public UserResourceBuilder WithName(string name)
        {
            userResource.Name = name;

            return this;
        }

        public UserResourceBuilder WithPhotoUrl(string photoUrl)
        {
            userResource.PhotoUrl = photoUrl;

            return this;
        }

        public UserResource Build() => userResource;
    }
}
