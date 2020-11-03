using System.Linq;
using ZPastel.API.Resources;
using ZPastel.Model;

namespace ZPastel.API.Converters
{
    public class PastelPageConverter
    {
        private readonly PastelConverter pastelConverter;

        public PastelPageConverter(PastelConverter pastelConverter)
        {
            this.pastelConverter = pastelConverter;
        }

        public PageResource<PastelResource> ConvertToResource(Page<Pastel> modelPage)
        {
            var itemsResource = modelPage.Items.Select(p => pastelConverter.ConvertToResource(p)).ToList();

            return new PageResource<PastelResource>(itemsResource, modelPage.HasMore);
        }
    }
}
