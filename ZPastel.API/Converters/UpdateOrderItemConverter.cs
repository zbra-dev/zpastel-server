using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class UpdateOrderItemConverter
    {
        public UpdateOrderItem ConvertToModel(long id, UpdateOrderItemResource updateOrderItemResource)
        {
            return new UpdateOrderItem
            {
                Id = id,
                Quantity = updateOrderItemResource.Quantity,
                ModifiedById = updateOrderItemResource.ModifiedById
            };
        }
    }
}
