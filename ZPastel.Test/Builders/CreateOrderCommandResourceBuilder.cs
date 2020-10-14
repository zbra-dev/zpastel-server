using ZPastel.API.Resources;

namespace ZPastel.Test.Builders
{
    public class CreateOrderCommandResourceBuilder
    {
        private readonly CreateOrderCommandResource createOrderCommandResource;

        public CreateOrderCommandResourceBuilder()
        {
            createOrderCommandResource = new CreateOrderCommandResource();
        }

        public CreateOrderCommandResourceBuilder WithDefaultValues()
        {
            createOrderCommandResource.CreatedById = 1;
            createOrderCommandResource.CreatedByUserName = "Tester";
            createOrderCommandResource.OrderItemResources = new[]
            {
                new CreateOrderItemResource
                {
                    CreatedById = 1,
                    Ingredients = "Queijo",
                    PastelId = 1,
                    Price = 4,
                    Quantity = 1,
                    Name = "4 Queijos"
                }
            };

            return this;
        }

        public CreateOrderCommandResource Build() => createOrderCommandResource;
    }
}
