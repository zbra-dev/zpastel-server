using System;

namespace ZPastel.Model
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long OrderId { get; }
        public long PastelId { get; }
        public DateTime Date { get; }
        public decimal Price { get; }
        public int Quantity { get; }
        public string Ingdredients { get; }

        public OrderItem(long orderId, long pastelId, decimal price, int quantity, string ingredients)
        {
            OrderId = orderId;
            PastelId = pastelId;
            Date = DateTime.Now;
            Price = price;
            Quantity = quantity;
            Ingdredients = ingredients;
        }
    }
}
