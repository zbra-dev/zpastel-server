namespace ZPastel.Model
{
    public class Drink : OrderItem
    {
        public string Name { get; set; }

        public Drink(string name, decimal price) : base(price)
        {
            Name = name;
        }
    }
}
