using System.Collections.Generic;

namespace ZPastel.Model
{
    public class Order
    {
        public int Id { get; set; }
        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public decimal TotalPrice { get; set; }
        public int ClientId { get; set; }
        public decimal DeliveryTax { get; set; }
    }
}
