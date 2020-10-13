using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class CreateOrderCommandConverter
    {
        public CreateOrderCommand ConvertToModel(CreateOrderCommandResource createOrderCommandResource)
        {
            return new CreateOrderCommand
            {

            };
        }
    }
}
