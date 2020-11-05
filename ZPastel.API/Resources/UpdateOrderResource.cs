using System.Collections.Generic;

namespace ZPastel.API.Resources
{
    public class UpdateOrderResource
    {
        public decimal TotalPrice { get; set; }
        public IList<UpdateOrderItemResource> OrderItems { get; set; } = new List<UpdateOrderItemResource>();
        public long ModifiedById { get; set; }
    }
}
