using System;

namespace ZPastel.Model
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long PastelId { get; set; }
        public Order Order { get; set; }
        public Pastel Pastel { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Ingredients { get; set; }
    }
}
