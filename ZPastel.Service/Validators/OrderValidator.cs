using System;
using System.Linq;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.Validators
{
    public class OrderValidator : IValidator<Order>
    {
        private readonly OrderItemValidator createOrderItemValidator;

        public OrderValidator(OrderItemValidator createOrderItemValidator)
        {
            this.createOrderItemValidator = createOrderItemValidator;
        }
        public async Task Validate(Order order)
        {
            if (order.CreatedById <= 0)
            {
                throw new ArgumentException("Invalid CreatedById");
            }
            if (string.IsNullOrEmpty(order.CreatedByUsername))
            {
                throw new ArgumentException("CreatedByUserName cannot be null or empty");
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
