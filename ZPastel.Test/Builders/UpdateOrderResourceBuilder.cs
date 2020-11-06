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
                    ModifiedById = 2,
                    Quantity = 2
                }
            };
            updateOrder.ModifiedById = 2;

            return this;
        }

        public UpdateOrderResource Build() => updateOrder;
    }
}
