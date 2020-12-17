using System;
using System.Linq;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Persistence.API.Repositories;
using ZPastel.Service.Exceptions;

namespace ZPastel.Service.Validators
{
    public class OrderValidator : IValidator<Order>
    {
        private readonly OrderItemValidator createOrderItemValidator;
        private readonly IUserRepository userRepository;

        public OrderValidator(OrderItemValidator createOrderItemValidator, IUserRepository userRepository)
        {
            this.createOrderItemValidator = createOrderItemValidator;
            this.userRepository = userRepository;
        }
        public async Task Validate(Order order)
        {
            var user = await userRepository.FindById(order.CreatedById);
            if (user == null)
            {
                throw new NotFoundException<User>(order.CreatedById.ToString(), nameof(order.CreatedById));
            }
            if (string.IsNullOrEmpty(order.CreatedByUsername))
            {
                throw new ArgumentException("CreatedByUserName cannot be null or empty");
            }
            if (order.TotalPrice < 0)
            {
                throw new ArgumentException("TotalPrice cannot be negative");
            }
            if (!order.OrderItems.Any())
            {
                throw new ArgumentException("At least one OrderItem is required");
            }
            else
            {
                foreach (var orderItem in order.OrderItems)
                {
                    await createOrderItemValidator.Validate(orderItem);
                }
            }
        }
    }
}
