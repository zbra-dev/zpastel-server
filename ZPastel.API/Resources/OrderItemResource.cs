using System;

namespace ZPastel.API.Resources
{
    public class OrderItemResource
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long PastelId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Ingredients { get; set; }
        public long CreatedById { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long LastModifiedById { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
