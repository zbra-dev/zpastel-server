using System;

namespace ZPastel.Model
{
    public class OrderItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long OrderItemId { get; set; }
        public long OrderId { get; set; }
        public long PastelId { get; set; }
        public Order Order { get; set; }
        public Pastel Pastel { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Ingredients { get; set; }
        public User User { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long LastModifiedById { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
