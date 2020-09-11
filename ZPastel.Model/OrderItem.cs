namespace ZPastel.Model
{
    public abstract class OrderItem
    {
        public int Id { get; set; }
        public string ClientNotes { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; }
        public string Description { get; set; }

        public OrderItem(decimal price)
        {
            Price = price;
        }
    }
}
