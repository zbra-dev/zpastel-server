namespace ZPastel.API.Resources
{
    public class UpdateOrderItemResource
    {
        public long Id { get; set; }
        public string Ingredients { get; set; }
        public long LastModifiedById { get; set; }
        public string Name { get; set; }
        public long PastelId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
