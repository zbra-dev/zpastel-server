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
using ZPastel.Test.Converters;
using ZPastel.Test.Extensions;
using ZPastel.Test.Utils;
using ZPastel.Tests;

namespace ZPastel.Test
{
    public class OrderEndpointUpdateOrderTests
    {
        private readonly CustomWebApplicationFactory factory;
        private readonly HttpClient client;

        public OrderEndpointUpdateOrderTests()
        {
            factory = new CustomWebApplicationFactory();
            client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        [Fact]
        public async Task UpdateOrder_WithValidId_ShouldUpdateOrder()
        {
            var updateOrderResource = new UpdateOrderResourceBuilder()
                .WithDefaultValues()
                .Build();

            var orderBeforeUpdating = await GetOrder(1);
            await UpdateOrder(1, updateOrderResource);
            var updatedOrder = await GetOrder(1);

            updatedOrder.TotalPrice.Should().Be(updateOrderResource.TotalPrice);
            updatedOrder.LastModifiedById.Should().Be(updateOrderResource.ModifiedById);

            var updatedOrderItem = updatedOrder.OrderItems.First(o => o.Id == 1);
            var updatedOrderItemResource = updateOrderResource.OrderItems.First(o => o.Id == 1);

            updatedOrderItem.LastModifiedById.Should().Be(updatedOrderItemResource.LastModifiedById);
            updatedOrderItem.Quantity.Should().Be(updatedOrderItemResource.Quantity);

            updatedOrder.Id.Should().Be(orderBeforeUpdating.Id);
            updatedOrder.CreatedByUsername.Should().Be(orderBeforeUpdating.CreatedByUsername);
            updatedOrder.CreatedById.Should().Be(orderBeforeUpdating.CreatedById);
            updatedOrder.CreatedOn.Should().Be(orderBeforeUpdating.CreatedOn);
        }

        private async Task<OrderResource> GetOrder(long id)
        {
            var response = await client.GetAsync($"api/orders/{id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            return await response.Deserialize<OrderResource>();
        }

        private async Task UpdateOrder(long id, UpdateOrderResource orderResource)
        {
            var content = SerializeUtils.Serialize(orderResource);
            var response = await client.PutAsync($"api/orders/edit/{id}", content);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateOrder_WithValidIdAndAddingNewOrderItems_ShouldUpdateOrder()
        {
            //TODO: change this when a 'GetPastelById()' method is available
            var pasteis = await GetPasteis();
            var pastelCamarao = pasteis.Single(p => p.Id == 3);
            var pastelDoceDeLeite = pasteis.Single(p => p.Id == 4);

            var firstOrderItemResource = new UpdateOrderItemResource
            {
                Ingredients = pastelCamarao.Ingredients,
                Name = pastelCamarao.Name,
                PastelId = pastelCamarao.Id,
                Price = pastelCamarao.Price,
                Quantity = 1
            };
            var secondOrderItemResource = new UpdateOrderItemResource
            {
                Ingredients = pastelDoceDeLeite.Ingredients,
                Name = pastelDoceDeLeite.Name,
                PastelId = pastelDoceDeLeite.Id,
                Price = pastelDoceDeLeite.Price,
                Quantity = 2
            };

            var updateOrderResource = new UpdateOrderResourceBuilder()
                .WithTotalPrice(2m)
                .WithOrderItems(new List<UpdateOrderItemResource> 
                { 
                    firstOrderItemResource,
                    secondOrderItemResource
                })
                .WithModifiedById(2)
                .Build();

            var orderBeforeUpdating = await GetOrder(1);
            //Should not delete previous OrderItems, so we add them to the orderResource
            var converter = new UpdateOrderItemResourceConverter();
            foreach (var orderItem in orderBeforeUpdating.OrderItems)
            {
                updateOrderResource.OrderItems.Add(converter.ConvertToUpdateItemResource(orderItem));
            }

            await UpdateOrder(1, updateOrderResource);
            var updatedOrder = await GetOrder(1);

            updatedOrder.TotalPrice.Should().Be(updateOrderResource.TotalPrice);
            updatedOrder.LastModifiedById.Should().Be(updateOrderResource.ModifiedById);
            updatedOrder.OrderItems.Count.Should().Be(updateOrderResource.OrderItems.Count);
            
            var firstAddedOrderItem = updatedOrder.OrderItems
                .Single(o => o.PastelId == firstOrderItemResource.PastelId);
            firstAddedOrderItem.Ingredients.Should().Be(firstOrderItemResource.Ingredients);
            firstAddedOrderItem.Name.Should().Be(firstOrderItemResource.Name);
            firstAddedOrderItem.PastelId.Should().Be(firstOrderItemResource.PastelId);
            firstAddedOrderItem.Price.Should().Be(firstOrderItemResource.Price);
            firstAddedOrderItem.Quantity.Should().Be(firstOrderItemResource.Quantity);
            firstAddedOrderItem.CreatedById.Should().Be(updateOrderResource.ModifiedById);
            firstAddedOrderItem.LastModifiedById.Should().Be(updateOrderResource.ModifiedById);

            var secondAddedOrderItem = updatedOrder.OrderItems
                .Single(o => o.PastelId == secondOrderItemResource.PastelId);
            secondAddedOrderItem.Ingredients.Should().Be(secondOrderItemResource.Ingredients);
            secondAddedOrderItem.Name.Should().Be(secondOrderItemResource.Name);
            secondAddedOrderItem.PastelId.Should().Be(secondOrderItemResource.PastelId);
            secondAddedOrderItem.Price.Should().Be(secondOrderItemResource.Price);
            secondAddedOrderItem.Quantity.Should().Be(secondOrderItemResource.Quantity);
            secondAddedOrderItem.CreatedById.Should().Be(updateOrderResource.ModifiedById);
            secondAddedOrderItem.LastModifiedById.Should().Be(updateOrderResource.ModifiedById);
        }

        private async Task<IReadOnlyCollection<PastelResource>> GetPasteis()
        {
            var response = await client.GetAsync("api/pasteis");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            return await response.Deserialize<IReadOnlyCollection<PastelResource>>();
        }

        [Fact]
        public async Task UpdateOrder_WithValidIdAndDeletingNewOrderItems_ShouldUpdateOrder()
        {
            var orderBeforeUpdating = await GetOrder(1);
            var orderItemToBeRemoved = orderBeforeUpdating.OrderItems.First();
            orderBeforeUpdating.OrderItems.Remove(orderItemToBeRemoved);

            var orderResource = new UpdateOrderResourceBuilder()
                .WithTotalPrice(2m)
                .WithModifiedById(2)
                .Build();

            var converter = new UpdateOrderItemResourceConverter();
            foreach (var orderItem in orderBeforeUpdating.OrderItems)
            {
                orderResource.OrderItems.Add(converter.ConvertToUpdateItemResource(orderItem));
            }

            await UpdateOrder(1, orderResource);
            var updatedOrder = await GetOrder(1);

            updatedOrder.TotalPrice.Should().Be(orderResource.TotalPrice);
            updatedOrder.LastModifiedById.Should().Be(orderResource.ModifiedById);
            updatedOrder.OrderItems.Count.Should().Be(orderResource.OrderItems.Count);
            updatedOrder.OrderItems.Should().NotContain(o => o.Id == orderItemToBeRemoved.Id);

            foreach (var orderItem in orderBeforeUpdating.OrderItems)
            {
                updatedOrder.OrderItems.Should().Contain(o => o.Id == orderItem.Id);
            }
        }
    }
}
