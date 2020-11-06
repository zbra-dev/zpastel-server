using System;
using System.Linq;
using System.Threading.Tasks;
using ZPastel.Core.Repositories;
using ZPastel.Model;
using ZPastel.Service.Exceptions;

namespace ZPastel.Service.Validators
{
    public class UpdateOrderValidator
    {
        private readonly IUserRepository userRepository;

        public UpdateOrderValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task Validate(UpdateOrder updateOrder)
        {
            if (updateOrder.TotalPrice < 0)
            {
                throw new ArgumentException("Total Price cannot be less than 0.");
            }
            if (!updateOrder.UpdateOrderItems.Any())
            {
                throw new ArgumentException("At least one Order Item is required.");
            }
            
            var user = await userRepository.FindById(updateOrder.LastModifiedById);

            if (user == null)
            {
                throw new NotFoundException<User>(updateOrder.LastModifiedById.ToString(), nameof(updateOrder.LastModifiedById));
            }
        }
    }
}
