using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class CreateOrderItemResourceConverter
    {
        public CreateOrderItem ConvertToModel(CreateOrderItemResource createOrderItemResource)
        {
            return new CreateOrderItem
            {
                CreatedById = createOrderItemResource.CreatedById,
                Ingredients = createOrderItemResource.Ingredients,
                Name = createOrderItemResource.Name,
                PastelId = createOrderItemResource.PastelId,
                Price = createOrderItemResource.Price,
                Quantity = createOrderItemResource.Quantity
            };
        }
    }
}
