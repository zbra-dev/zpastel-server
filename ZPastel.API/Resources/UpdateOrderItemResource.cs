namespace ZPastel.API.Resources
{
    public class UpdateOrderItemResource
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public long ModifiedById { get; set; }
    }
}
