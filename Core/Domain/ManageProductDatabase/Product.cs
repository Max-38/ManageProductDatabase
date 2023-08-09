namespace ManageProductDatabase
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal ItemsCount { get; set; }

        public Product(int id, string name, decimal price, decimal itemsCount)
        {
            Id = id;
            Name = name;
            Price = price;
            ItemsCount = itemsCount;
        }
    }
}
