using System.Collections.Generic;
using System.Linq;
using ZPastel.Model;
using ZPastel.Service.Contracts;

namespace ZPastel.Service.Impl
{
    public class OrderService : IOrderService
    {
        private int IdGenerator = 1;
        public Order CreateOrder(IList<OrderItem> orderItems, int clientId)
        {
            return new Order
            {
                ClientId = clientId,
                DeliveryTax = 0m,
                Id = IdGenerator++,
                OrderItems = orderItems,
                TotalPrice = CalculateTotalPrice(orderItems),
            };
        }

        private decimal CalculateTotalPrice(IList<OrderItem> orderItems)
        {
            return orderItems.Sum(o => o.Price * o.Quantity);
        }
    }
}
