namespace ZPastel.API.Resources
{
    public class PastelResource
    {
        public long Id { get; set; }
        public long PasteleiroId { get; set; }
        public bool IsAvailable { get; set; }
        public string Ingdredients { get; set; }
        public decimal Price { get; set; }
    }
}
