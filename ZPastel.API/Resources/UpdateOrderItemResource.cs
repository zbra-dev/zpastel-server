namespace ZPastel.API.Resources
{
    public class UpdateOrderItemResource
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Ingredients { get; set; }
        public long ModifiedById { get; set; }
    }
}
