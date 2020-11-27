using ZPastel.Model;

namespace ZPastel.Test.Converters
{
    internal class UpdateOrderConverter
    {
        public UpdateOrder ConvertToUpdateOrder(Order order)
        {
            return new UpdateOrder
            {
                TotalPrice = order.TotalPrice,
                UpdateOrderItems = order.OrderItems
            };
        }
    }
}
