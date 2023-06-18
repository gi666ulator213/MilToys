namespace MilToys {
    public class Toy {
        public int Id { get; }
        public string Name { get; }
        public string Stock { get; }
        public string Age { get; set; }
        public int Price { get; set; }

        public Toy(int id, string name, string stock, string age, int price) {
            Id = id;
            Name = name;
            Stock = stock;
            Age = age;
            Price = price;
        }
    }
}
