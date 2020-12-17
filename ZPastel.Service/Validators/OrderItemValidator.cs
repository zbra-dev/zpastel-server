using System;
using System.Threading.Tasks;
using ZPastel.Model;
using ZPastel.Persistence.API.Repositories;
using ZPastel.Service.Exceptions;

namespace ZPastel.Service.Validators
{
    public class OrderItemValidator : IValidator<OrderItem>
    {
        private readonly IPastelRepository pastelRepository;
        private readonly IUserRepository userRepository;

        public OrderItemValidator(IPastelRepository pastelRepository, IUserRepository userRepository)
        {
            this.pastelRepository = pastelRepository;
            this.userRepository = userRepository;
        }

        public async Task Validate(OrderItem orderItem)
        {
            var user = await userRepository.FindById(orderItem.CreatedById);
            if (user == null)
            {
                throw new NotFoundException<User>(orderItem.CreatedById.ToString(), nameof(orderItem.CreatedById));
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
            if (orderItem.Price < 0)
            {
                throw new ArgumentException("OrderItem Price must be greater or equal than 0");
            }

            var pastel = await pastelRepository.FindById(orderItem.PastelId);
            if (pastel == null)
            {
                throw new NotFoundException<Pastel>(orderItem.PastelId.ToString(), nameof(orderItem.PastelId));
            }
        }
    }
}
