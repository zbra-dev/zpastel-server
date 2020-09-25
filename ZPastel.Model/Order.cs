using System;
using System.Collections.Generic;

namespace ZPastel.Model
{
    public class Order
    {
        public long Id { get; set; }
        public DateTime Date { get; }
        public double TotalPrice { get; set; }
        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
