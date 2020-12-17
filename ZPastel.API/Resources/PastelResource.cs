using System;

namespace ZPastel.API.Resources
{
    public class PastelResource
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }
        public long CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long LastModifiedById { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string FlavorImageUrl { get; set; }
    }
}
