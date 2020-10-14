using System.Linq;
using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class CreateOrderCommandConverter
    {
        private readonly CreateOrderItemResourceConverter createOrderItemResourceConverter;

        public CreateOrderCommandConverter(CreateOrderItemResourceConverter createOrderItemResourceConverter)
        {
            this.createOrderItemResourceConverter = createOrderItemResourceConverter;
        }

        public CreateOrderCommand ConvertToModel(CreateOrderCommandResource createOrderCommandResource)
        {
            return new CreateOrderCommand
            {
                CreatedById = createOrderCommandResource.CreatedById,
                CreatedByUserName = createOrderCommandResource.CreatedByUserName,
                OrderItems = createOrderCommandResource.OrderItemResources.Select(i => createOrderItemResourceConverter.ConvertToModel(i)).ToList()
            };
        }
    }
}
