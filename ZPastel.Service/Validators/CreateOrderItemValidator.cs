using System;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Service.Exceptions;
using ZPastel.Service.Repositories;

namespace ZPastel.Service.Validators
{
    public class CreateOrderItemValidator : IValidator<CreateOrderItem>
    {
        private readonly IPastelRepository pastelRepository;

        public CreateOrderItemValidator(IPastelRepository pastelRepository)
        {
            this.pastelRepository = pastelRepository;
        }

        public async Task Validate(CreateOrderItem createOrderItem)
        {
            if (createOrderItem.CreatedById <= 0)
            {
                throw new ArgumentException("Invalid OrderItem CreatedById");
            }
            if (string.IsNullOrEmpty(createOrderItem.Ingredients))
            {
                throw new ArgumentException("OrderItem Ingredients cannot be null or empty");
            }
            if (string.IsNullOrEmpty(createOrderItem.Name))
            {
                throw new ArgumentException("OrderItem name cannot be null or empty");
            }
            if (createOrderItem.Quantity <= 0)
            {
                throw new ArgumentException("OrderItem Quantity must be greater than 0");
            }

            var pastel = await pastelRepository.FindById(createOrderItem.PastelId);
            if (pastel == null)
            {
                throw new NotFoundException<Pastel>(createOrderItem.PastelId.ToString(), nameof(createOrderItem.PastelId));
            }
        }
    }
}
