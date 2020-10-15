﻿namespace ZPastel.API.Resources
{
    public class CreateOrderItemResource
    {
        public long PastelId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Ingredients { get; set; }
        public long CreatedById { get; set; }
    }
}