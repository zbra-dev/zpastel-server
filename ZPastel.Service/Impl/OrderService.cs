using System.Collections.Generic;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Service.Contract;
using ZPastel.Service.Repositories;

namespace ZPastel.Service.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task CreateOrder(CreateOrderCommand createOrderCommand)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<Order>> FindAll()
        {
            return await orderRepository.FindAll();
        }

        public async Task<Order> FindById(long id)
        {
            return await orderRepository.FindById(id);
        }
    }
}
