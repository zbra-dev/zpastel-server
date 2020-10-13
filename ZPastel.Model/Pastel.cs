using System;

namespace ZPastel.Model
{
    public class Pastel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long PasteleiroId { get; set; }
        public Pasteleiro Pasteleiro { get; set; }
        public bool IsAvailable { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }
        public User User { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long LastModifiedById { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
