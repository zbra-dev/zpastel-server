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
                CreatedById = orderItem.CreatedById,
                CreatedOn = orderItem.CreatedOn,
                Id = orderItem.Id,
                Ingredients = orderItem.Ingredients,
                LastModifiedById = orderItem.LastModifiedById,
                LastModifiedOn = orderItem.LastModifiedOn,
                OrderId = orderItem.OrderId,
                PastelId = orderItem.PastelId,
                Price = orderItem.Price,
                Quantity = orderItem.Quantity
            };
        }
    }
}
