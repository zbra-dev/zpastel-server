namespace ZPastel.Model
{
    public class UpdateOrderItem
    {
        public long Id { get; set; }
        public int Quantity { get; set; }         
        public long ModifiedById { get; set; }
    }
}
