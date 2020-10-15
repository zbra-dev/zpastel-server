using System;
using System.Threading.Tasks;
using ZPastel.Core.Repositories;
using ZPastel.Model;
using ZPastel.Service.Exceptions;

namespace ZPastel.Service.Validators
{
    public class OrderItemValidator : IValidator<OrderItem>
    {
        private readonly IPastelRepository pastelRepository;

        public OrderItemValidator(IPastelRepository pastelRepository)
        {
            this.pastelRepository = pastelRepository;
        }

        public async Task Validate(OrderItem orderItem)
        {
            if (orderItem.CreatedById <= 0)
            {
                throw new ArgumentException("Invalid OrderItem CreatedById");
            }
            if (string.IsNullOrEmpty(orderItem.Ingredients))
            {
                throw new ArgumentException("OrderItem Ingredients cannot be null or empty");
            }
            if (string.IsNullOrEmpty(orderItem.Name))
            {
                throw new ArgumentException("OrderItem name cannot be null or empty");
            }
            if (orderItem.Quantity <= 0)
            {
                throw new ArgumentException("OrderItem Quantity must be greater than 0");
            }

            var pastel = await pastelRepository.FindById(orderItem.PastelId);
            if (pastel == null)
            {
                throw new NotFoundException<Pastel>(orderItem.PastelId.ToString(), nameof(orderItem.PastelId));
            }
        }
    }
}
