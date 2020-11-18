using System.Collections.Generic;

namespace ZPastel.Model
{
    public class UpdateOrder
    {
        public decimal TotalPrice { get; set; }
        public IList<OrderItem> UpdateOrderItems { get; set; } = new List<OrderItem>();
        public long LastModifiedById { get; set; }
    }
}
