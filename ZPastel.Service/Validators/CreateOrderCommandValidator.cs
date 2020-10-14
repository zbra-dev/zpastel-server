using System;
using System.Linq;
using System.Threading.Tasks;
using ZPastel.Model;

namespace ZPastel.Service.Validators
{
    public class CreateOrderCommandValidator : IValidator<CreateOrderCommand>
    {
        private readonly CreateOrderItemValidator createOrderItemValidator;

        public CreateOrderCommandValidator(CreateOrderItemValidator createOrderItemValidator)
        {
            this.createOrderItemValidator = createOrderItemValidator;
        }
        public async Task Validate(CreateOrderCommand createOrderCommand)
        {
            if (createOrderCommand.CreatedById <= 0)
            {
                throw new ArgumentException("Invalid CreatedById");
            }
            if (string.IsNullOrEmpty(createOrderCommand.CreatedByUserName))
            {
                throw new ArgumentException("CreatedByUserName cannot be null or empty");
            }
            if (!createOrderCommand.OrderItems.Any())
            {
                throw new ArgumentException("At least one OrderItem is required");
            }
            else
            {
                foreach (var orderItem in createOrderCommand.OrderItems)
                {
                    await createOrderItemValidator.Validate(orderItem);
                }
            }
        }
    }
}
