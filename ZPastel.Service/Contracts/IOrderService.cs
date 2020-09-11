using System.Collections.Generic;
using ZPastel.Model;

namespace ZPastel.Service.Contracts
{
    public interface IOrderService
    {
        Order CreateOrder(IList<OrderItem> orderItems, int clientId);
    }
}
