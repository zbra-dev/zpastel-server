using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class PastelFilterConverter
    {
        public PastelFilter ConvertToModel(PastelFilterResource pastelFilterResource)
        {
            return new PastelFilter
            {
                Name = pastelFilterResource.Name,
                Skip = pastelFilterResource.Skip,
                Take = pastelFilterResource.Take,
            };
        }
    }
}
