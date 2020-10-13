using System;
using System.Collections.Generic;

namespace ZPastel.API.Resources
{
    public class OrderResource
    {
        public long Id { get; set; }
        public decimal TotalPrice { get; set; }
        public IList<OrderItemResource> OrderItems { get; set; } = new List<OrderItemResource>();
        public string CreatedByUsername { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long LastModifiedById { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
