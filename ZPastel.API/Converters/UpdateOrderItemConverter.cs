using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class UpdateOrderItemConverter
    {
        public OrderItem ConvertToModel(long id, UpdateOrderItemResource updateOrderItemResource)
        {
            return new OrderItem
            {
                Id = id,
                Ingredients = updateOrderItemResource.Ingredients,
                LastModifiedById = updateOrderItemResource.LastModifiedById,
                Name = updateOrderItemResource.Name,
                PastelId = updateOrderItemResource.PastelId,
                Price = updateOrderItemResource.Price,
                Quantity = updateOrderItemResource.Quantity
            };
        }
    }
}
