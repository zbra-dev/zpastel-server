using System.Collections.Generic;

namespace ZPastel.API.Resources
{
    public class CreateOrderCommandResource
    {
        public long CreatedById { get; set; }
        public string CreatedByUserName { get; set; }
        public IList<CreateOrderItemResource> OrderItemResources { get; set; } = new List<CreateOrderItemResource>();
    }
}
