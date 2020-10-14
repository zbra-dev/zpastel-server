using System.Collections.Generic;

namespace ZPastel.Model
{
    public class CreateOrderCommand
    {
        public long CreatedById { get; set; }
        public string CreatedByUserName { get; set; }
        public IList<CreateOrderItem> OrderItems { get; set; } = new List<CreateOrderItem>();
    }
}
