namespace ZPastel.Model
{
    public class Pastel : OrderItem
    {
        public string Flavor { get; }

        public Pastel(string flavor, decimal price) : base(price)
        {
            Flavor = flavor;
        }
    }
}
