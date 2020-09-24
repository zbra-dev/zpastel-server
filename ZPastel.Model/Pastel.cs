namespace ZPastel.Model
{
    public class Pastel
    {
        public long Id { get; set; }
        public long PasteleiroId { get; set; }
        public bool IsAvailable { get; set; }
        public string Ingdredients { get; }
        public decimal Price { get; }

        public Pastel(string ingredients, decimal price)
        {
            Ingdredients = ingredients;
            Price = price;
        }
    }
}
