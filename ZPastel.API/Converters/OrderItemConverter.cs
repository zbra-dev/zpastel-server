using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class OrderItemConverter
    {
        public OrderItemResource ConvertToResource(OrderItem orderItem)
        {
            return new OrderItemResource
            {
                Id = orderItem.Id,
                CreatedById = orderItem.CreatedById,
                Ingredients = orderItem.Ingredients,
                PastelId = orderItem.PastelId,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity,
                LastModifiedById = orderItem.LastModifiedById,
                CreatedOn = orderItem.CreatedOn,
                LastModifiedOn = orderItem.LastModifiedOn,
                OrderId = orderItem.OrderId
            };
        }
    }
}
