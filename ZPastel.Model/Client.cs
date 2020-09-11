namespace ZPastel.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; } = new byte[] { };
    }
}
