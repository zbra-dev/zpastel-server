using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ZPastel.API.Resources;
using ZPastel.Test.Extensions;
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

            var pasteis = await response.Deserialize<IReadOnlyCollection<PastelResource>>();
            pasteis.Count.Should().Be(4);

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

            var thirdPastel = pasteis.Skip(2).First();

            thirdPastel.Id.Should().Be(3);
            thirdPastel.Name.Should().Be("Camarao");
            thirdPastel.Ingredients.Should().Be("Camarao fresco");
            thirdPastel.Price.Should().Be(8.00m);
            thirdPastel.CreatedById.Should().Be(1);
            thirdPastel.LastModifiedById.Should().Be(1);

            var fourthPastel = pasteis.Skip(3).First();

            fourthPastel.Id.Should().Be(4);
            fourthPastel.Name.Should().Be("Doce de leite");
            fourthPastel.Ingredients.Should().Be("Doce de leite");
            fourthPastel.Price.Should().Be(8.00m);
            fourthPastel.CreatedById.Should().Be(1);
            fourthPastel.LastModifiedById.Should().Be(1);
        }

        private HttpClient GetClient()
        {
            return factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }
    }
}
