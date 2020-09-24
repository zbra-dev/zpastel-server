using System;

namespace ZPastel.Model
{
    public class Order
    {
        public long Id { get; set; }
        public DateTime Date { get; }
        public double TotalPrice { get; set; }
    }
}
