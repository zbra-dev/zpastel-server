namespace ZPastel.Model
{
    public class Pastel
    {
        public long Id { get; set; }
        public long PasteleiroId { get; set; }
        public Pasteleiro Pasteleiro { get; set; }
        public bool IsAvailable { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }
    }
}
