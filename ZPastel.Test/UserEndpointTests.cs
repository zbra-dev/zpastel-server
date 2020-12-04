using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using ZPastel.API.Resources;
using ZPastel.Test.Builders;
using ZPastel.Tests;


namespace ZPastel.Test
{
    public class UserEndpointTests
    {
        private readonly CustomWebApplicationFactory factory;
        public UserEndpointTests()
        {
            factory = new CustomWebApplicationFactory();
        }

        [Fact]
        public async Task FindUserByFirebaseId_WithValidId_ShouldReturnCorrectUser()
        {
            var client = GetClient();
            var response = await client.GetAsync("api/users/user-1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var userContent = await response.Content.ReadAsStringAsync();
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserResource>(userContent);

            user.Should().NotBeNull();
            user.CreatedById.Should().Be(1);
            user.CreatedOn.Should().BeAfter(DateTime.MinValue);
            user.Email.Should().Be("user@test.com");
            user.FirebaseId.Should().Be("user-1");
            user.Id.Should().Be(1);
            user.LastModifiedById.Should().Be(1);
            user.LastModifiedOn.Should().BeAfter(DateTime.MinValue);
            user.Name.Should().Be("User");
            user.PhotoUrl.Should().Be("www.photourl.com/user.jpg");
        }

        private HttpClient GetClient()
        {
            return factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        [Fact]
        public async Task FindUserByFirebaseId_WithInvalidValidId_ShouldReturnNull()
        {
            var client = GetClient();
            var response = await client.GetAsync("api/users/invalid");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var userContent = await response.Content.ReadAsStringAsync();
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserResource>(userContent);

            user.Should().BeNull();
        }

        [Fact]
        public async Task SaveUser_WithUserNotInRepository_ShouldCreateUser()
        {
            var command = new UserResourceBuilder()
                .WithDefaultValues()
                .Build();

            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var client = GetClient();

            var createResponse = await client.PostAsync("api/users/save", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdUserContent = await createResponse.Content.ReadAsStringAsync();
            var userResource = Newtonsoft.Json.JsonConvert.DeserializeObject<UserResource>(createdUserContent);

            userResource.Should().NotBeNull();
            userResource.CreatedById.Should().Be(command.CreatedById);
            userResource.CreatedOn.Should().Be(command.CreatedOn);
            userResource.Email.Should().Be(command.Email);
            userResource.FirebaseId.Should().Be(command.FirebaseId);
            userResource.Id.Should().BeGreaterThan(0);
            userResource.LastModifiedById.Should().Be(command.LastModifiedById);
            userResource.LastModifiedOn.Should().Be(command.LastModifiedOn);
            userResource.Name.Should().Be(command.Name);
            userResource.PhotoUrl.Should().Be(command.PhotoUrl);
        }

        [Fact]
        public async Task SaveUser_WithUserInRepository_ShouldUpdateUser()
        {
            var client = GetClient();
            var response = await client.GetAsync("api/users/user-1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var userContent = await response.Content.ReadAsStringAsync();
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserResource>(userContent);

            user.Email = "newemail@test.com";
            user.LastModifiedById = 2;
            var now = DateTime.Now;
            user.LastModifiedOn = now;
            user.Name = "New Name";
            user.PhotoUrl = "www.newurl.com/photo.jpg";

            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var createResponse = await client.PostAsync("api/users/save", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var createdUserContent = await createResponse.Content.ReadAsStringAsync();
            var userResource = Newtonsoft.Json.JsonConvert.DeserializeObject<UserResource>(createdUserContent);

            userResource.Should().NotBeNull();
            userResource.CreatedById.Should().Be(1);
            userResource.CreatedOn.Should().BeAfter(DateTime.MinValue);
            userResource.Email.Should().Be("newemail@test.com");
            userResource.FirebaseId.Should().Be("user-1");
            userResource.Id.Should().Be(1);
            userResource.LastModifiedById.Should().Be(2);
            userResource.LastModifiedOn.Should().Be(now);
            userResource.Name.Should().Be("New Name");
            userResource.PhotoUrl.Should().Be("www.newurl.com/photo.jpg");
        }

        [Fact]
        public async Task SaveUser_WithEmptyName_ShouldThrowArgumentException()
        {
            var command = new UserResourceBuilder()
                .WithDefaultValues()
                .WithName(string.Empty)
                .Build();

            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var client = GetClient();

            var createResponse = await client.PostAsync("api/users/save", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task SaveUser_WithEmptyEmail_ShouldThrowArgumentException()
        {
            var command = new UserResourceBuilder()
                .WithDefaultValues()
                .WithEmail(string.Empty)
                .Build();

            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var client = GetClient();

            var createResponse = await client.PostAsync("api/users/save", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task SaveUser_WithEmptyPhotoUrl_ShouldThrowArgumentException()
        {
            var command = new UserResourceBuilder()
                .WithDefaultValues()
                .WithPhotoUrl(string.Empty)
                .Build();

            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var client = GetClient();

            var createResponse = await client.PostAsync("api/users/save", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
