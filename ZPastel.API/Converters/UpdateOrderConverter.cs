using System.Linq;
using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class UpdateOrderConverter
    {
        private readonly UpdateOrderItemConverter updateOrderItemConverter;

        public UpdateOrderConverter(UpdateOrderItemConverter updateOrderItemConverter)
        {
            this.updateOrderItemConverter = updateOrderItemConverter;
        }

        public UpdateOrder ConvertToModel(UpdateOrderResource orderResource)
        {
            return new UpdateOrder
            {
                TotalPrice = orderResource.TotalPrice,
                UpdateOrderItems = orderResource.OrderItems
                    .Select(i => updateOrderItemConverter.ConvertToModel(i.Id, i))
                    .ToList(),
                LastModifiedById = orderResource.ModifiedById,
            };
        }
    }
}
