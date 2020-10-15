using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ZPastel.API.Resources;
using ZPastel.Tests;

namespace ZPastel.Test
{
    public class PastelEndpointTests
    {
        private readonly HttpClient client;

        public PastelEndpointTests()
        {
            var factory = new CustomWebApplicationFactory();

            client = factory
                .CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        [Fact]
        public async Task GetPasteis_AllFlavors_ShouldReturnAllPasteis()
        {
            var response = await client.GetAsync("api/pasteis");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var pasteisContent = await response.Content.ReadAsStringAsync();
            var pasteis = Newtonsoft.Json.JsonConvert.DeserializeObject<IReadOnlyCollection<PastelResource>>(pasteisContent);

            pasteis.Count.Should().Be(2);

            var firstPastel = pasteis.First();

            firstPastel.Id.Should().Be(1);
            firstPastel.Name.Should().Be("4 Queijos");
            firstPastel.Ingredients.Should().Be("Mussarela, Cheddar, Provolone, Catupiry");
            firstPastel.Price.Should().Be(5);
            firstPastel.CreatedById.Should().Be(1);
            firstPastel.LastModifiedById.Should().Be(1);

            var secondPastel = pasteis.Skip(1).First();

            secondPastel.Id.Should().Be(2);
            secondPastel.Name.Should().Be("Carne");
            secondPastel.Ingredients.Should().Be("Carne Moida");
            secondPastel.Price.Should().Be(4.50m);
            secondPastel.CreatedById.Should().Be(1);
            secondPastel.LastModifiedById.Should().Be(1);
        }
    }
}
