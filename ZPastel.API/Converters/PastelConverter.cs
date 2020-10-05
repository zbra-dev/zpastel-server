using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class PastelConverter
    {
        public PastelResource ConvertToResource(Pastel pastel)
        {
            return new PastelResource
            {
                Id = pastel.Id,
                Ingdredients = pastel.Ingredients,
                IsAvailable = pastel.IsAvailable,
                PasteleiroId = pastel.PasteleiroId,
                Price = pastel.Price
            };
        }
    }
}
