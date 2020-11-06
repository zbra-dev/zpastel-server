using System.Collections.Generic;

namespace ZPastel.Model
{
    public class UpdateOrder
    {
        public decimal TotalPrice { get; set; }
        public IList<UpdateOrderItem> UpdateOrderItems { get; set; } = new List<UpdateOrderItem>();
        public long LastModifiedById { get; set; }
    }
}
