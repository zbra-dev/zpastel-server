using ZPastel.API.Resources;

namespace ZPastel.Test.Builders
{
    public class FilterPastelResourceBuilder
    {
        private readonly PastelFilterResource pastelFilterResource;

        public FilterPastelResourceBuilder()
        {
            pastelFilterResource = new PastelFilterResource();
        }

        public FilterPastelResourceBuilder WithDefaultValues()
        {
            pastelFilterResource.Name = "Pastel";

            return this;
        }

        public FilterPastelResourceBuilder WithName(string name)
        {
            pastelFilterResource.Name = name;

            return this;
        }

        public PastelFilterResource Build() => pastelFilterResource;
    }
}
