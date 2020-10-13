using System.Linq;
using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class OrderConverter
    {
        private readonly OrderItemConverter orderItemConverter;

        public OrderConverter(OrderItemConverter orderItemConverter)
        {
            this.orderItemConverter = orderItemConverter;
        }

        public OrderResource ConvertToResource(Order order)
        {
            return new OrderResource
            {
                CreatedById = order.CreatedById,
                CreatedByUsername = order.CreatedByUsername,
                CreatedOn = order.CreatedOn,
                Id = order.Id,
                LastModifiedById = order.LastModifiedById,
                LastModifiedOn = order.LastModifiedOn,
                OrderItems = order.OrderItems.Select(i => orderItemConverter.ConvertToResource(i)).ToList(),
                TotalPrice = order.TotalPrice
            };
        }
    }
}
