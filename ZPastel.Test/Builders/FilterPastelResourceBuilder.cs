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
            pastelFilterResource.Take = 25;
            pastelFilterResource.Skip = 0;
            pastelFilterResource.Name = "Pastel";

            return this;
        }

        public FilterPastelResourceBuilder WithName(string name)
        {
            pastelFilterResource.Name = name;

            return this;
        }

        public FilterPastelResourceBuilder WithTake(int take)
        {
            pastelFilterResource.Take = take;

            return this;
        }

        public FilterPastelResourceBuilder WithSkip(int skip)
        {
            pastelFilterResource.Skip = skip;

            return this;
        }

        public PastelFilterResource Build() => pastelFilterResource;
    }
}
