using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Linq;
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
    public class PastelEndpointTests
    {
        private readonly CustomWebApplicationFactory factory;
        public PastelEndpointTests()
        {
            factory = new CustomWebApplicationFactory();
        }

        [Fact]
        public async Task GetPasteis_AllFlavors_ShouldReturnAllPasteis()
        {
            var client = GetClient();
            var response = await client.GetAsync("api/pasteis");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var pasteisContent = await response.Content.ReadAsStringAsync();
            var pasteis = Newtonsoft.Json.JsonConvert.DeserializeObject<IReadOnlyCollection<PastelResource>>(pasteisContent);

            pasteis.Count.Should().Be(3);

            var firstPastel = pasteis.First();

            firstPastel.Id.Should().Be(1);
            firstPastel.Name.Should().Be("Pastel de 4 Queijos");
            firstPastel.Ingredients.Should().Be("Mussarela, Cheddar, Provolone, Catupiry");
            firstPastel.Price.Should().Be(5);
            firstPastel.CreatedById.Should().Be(1);
            firstPastel.LastModifiedById.Should().Be(1);

            var secondPastel = pasteis.Skip(1).First();

            secondPastel.Id.Should().Be(2);
            secondPastel.Name.Should().Be("Pastel de Carne");
            secondPastel.Ingredients.Should().Be("Carne Moida");
            secondPastel.Price.Should().Be(4.50m);
            secondPastel.CreatedById.Should().Be(1);
            secondPastel.LastModifiedById.Should().Be(1);

            var thirdPastel = pasteis.Skip(2).First();

            thirdPastel.Id.Should().Be(3);
            thirdPastel.Name.Should().Be("Pastel de Vento");
            thirdPastel.Ingredients.Should().Be("Vento");
            thirdPastel.Price.Should().Be(3);
            thirdPastel.CreatedById.Should().Be(1);
            thirdPastel.LastModifiedById.Should().Be(1);
        }

        private HttpClient GetClient()
        {
            return factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        [Theory]
        [InlineData("4 Queijos")]
        [InlineData("queijo")]
        [InlineData("4 QUeiJOs")]
        [InlineData("carne")]
        [InlineData("CARNE")]
        [InlineData("CarNE")]
        [InlineData("PASTEL DE CARNE")]
        [InlineData("pastel de 4 queijos")]
        public async Task FilterPasteis_ByName_ShouldReturnCorrectPasteis(string pastelName)
        {
            var command = new FilterPastelResourceBuilder()
                .WithDefaultValues()
                .WithName(pastelName)
                .Build();

            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var client = GetClient();

            var createResponse = await client.PostAsync("api/pasteis/filter", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var filterPasteisContent = await createResponse.Content.ReadAsStringAsync();
            var filterPasteisResource = Newtonsoft.Json.JsonConvert.DeserializeObject<PageResource<PastelResource>>(filterPasteisContent);

            filterPasteisResource.Items.Should().HaveCount(1);
        }

        [Theory]
        [InlineData("PASTEL")]
        [InlineData("pastel")]
        [InlineData("")]
        public async Task FilterPasteis_WithNameCornerCases_ShouldReturnAllPasteis(string name)
        {
            var command = new FilterPastelResourceBuilder()
                .WithDefaultValues()
                .WithName(name)
                .Build();

            var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            var client = GetClient();

            var createResponse = await client.PostAsync("api/pasteis/filter", content);
            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var filterPasteisContent = await createResponse.Content.ReadAsStringAsync();
            var filterPasteisResource = Newtonsoft.Json.JsonConvert.DeserializeObject<PageResource<PastelResource>>(filterPasteisContent);

            filterPasteisResource.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task PaginationTests_WithTakeOne_ShouldCallFilterTwiceAndReturnAllPasteis()
        {
            var command = new FilterPastelResourceBuilder()
                .WithTake(1)
                .Build();

            var serverNumberOfTimesCalled = 0;
            var pasteis = new List<PastelResource>();
            PageResource<PastelResource> filterPastelResource;

            var client = GetClient();

            do
            {
                var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");
                var createResponse = await client.PostAsync("api/pasteis/filter", content);
                createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                var filterPasteisContent = await createResponse.Content.ReadAsStringAsync();
                filterPastelResource = Newtonsoft.Json.JsonConvert.DeserializeObject<PageResource<PastelResource>>(filterPasteisContent);
                pasteis.AddRange(filterPastelResource.Items);
                command.Skip = pasteis.Count();
                serverNumberOfTimesCalled++;
            } while (filterPastelResource.HasMore);

            serverNumberOfTimesCalled.Should().Be(2);
            pasteis.Should().HaveCount(2);

            var firstPastelFound = pasteis.First();
            firstPastelFound.Id.Should().Be(2);
            firstPastelFound.Name.Should().Be("Pastel de Carne");

            var secondPastelFound = pasteis.Skip(1).First();
            secondPastelFound.Id.Should().Be(1);
            secondPastelFound.Name.Should().Be("Pastel de 4 Queijos");
        }

        [Fact]
        public async Task PaginationTests_WithTakeTwo_ShouldCallFilterOnceAndReturnAllPasteis()
        {
            var command = new FilterPastelResourceBuilder()
                .WithTake(2)
                .Build();

            var serverNumberOfTimesCalled = 0;
            var pasteis = new List<PastelResource>();
            PageResource<PastelResource> filterPastelResource;

            var client = GetClient();

            do
            {
                var content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");
                var createResponse = await client.PostAsync("api/pasteis/filter", content);
                createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

                var filterPasteisContent = await createResponse.Content.ReadAsStringAsync();
                filterPastelResource = Newtonsoft.Json.JsonConvert.DeserializeObject<PageResource<PastelResource>>(filterPasteisContent);
                pasteis.AddRange(filterPastelResource.Items);
                command.Skip = pasteis.Count();
                serverNumberOfTimesCalled++;
            } while (filterPastelResource.HasMore);

            serverNumberOfTimesCalled.Should().Be(1);
            pasteis.Should().HaveCount(2);

            var firstPastelFound = pasteis.First();
            firstPastelFound.Id.Should().Be(2);
            firstPastelFound.Name.Should().Be("Pastel de Carne");

            var secondPastelFound = pasteis.Skip(1).First();
            secondPastelFound.Id.Should().Be(1);
            secondPastelFound.Name.Should().Be("Pastel de 4 Queijos");
        }
    }
}
