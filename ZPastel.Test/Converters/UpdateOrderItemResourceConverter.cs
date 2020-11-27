using ZPastel.API.Resources;

namespace ZPastel.Test.Converters
{
    internal class UpdateOrderItemResourceConverter
    {
        public UpdateOrderItemResource ConvertToUpdateItemResource(OrderItemResource orderItemResource)
        {
            return new UpdateOrderItemResource
            {
                Id = orderItemResource.Id,
                Ingredients = orderItemResource.Ingredients,
                LastModifiedById = orderItemResource.LastModifiedById,
                Name = orderItemResource.Name,
                PastelId = orderItemResource.PastelId,
                Price = orderItemResource.Price,
                Quantity = orderItemResource.Quantity
            };
        }
    }
}
