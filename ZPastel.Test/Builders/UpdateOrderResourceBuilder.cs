using System.Collections.Generic;
using ZPastel.API.Resources;

namespace ZPastel.Test.Builders
{
    class UpdateOrderResourceBuilder
    {
        private readonly UpdateOrderResource updateOrder;

        public UpdateOrderResourceBuilder()
        {
            updateOrder = new UpdateOrderResource();
        }

        public UpdateOrderResourceBuilder WithDefaultValues()
        {
            updateOrder.TotalPrice = 50.0m;
            updateOrder.OrderItems = new[]
            {
                new UpdateOrderItemResource
                {
                    Id = 1,
                    LastModifiedById = 2,
                    Quantity = 2
                }
            };
            updateOrder.ModifiedById = 2;

            return this;
        }

        public UpdateOrderResourceBuilder WithTotalPrice(decimal price)
        {
            updateOrder.TotalPrice = price;
            return this;
        }

        public UpdateOrderResourceBuilder WithOrderItems(IList<UpdateOrderItemResource> orderItemResources)
        {
            updateOrder.OrderItems = orderItemResources;
            return this;
        }

        public UpdateOrderResourceBuilder WithModifiedById(long id)
        {
            updateOrder.ModifiedById = id;
            return this;
        }

        public UpdateOrderResource Build() => updateOrder;
    }
}
