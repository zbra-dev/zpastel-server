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
                Ingredients = pastel.Ingredients,
                Price = pastel.Price,
                CreatedById = pastel.CreatedById,
                CreatedOn = pastel.CreatedOn,
                LastModifiedById = pastel.LastModifiedById,
                LastModifiedOn = pastel.LastModifiedOn,
                Name = pastel.Name
            };
        }
    }
}
