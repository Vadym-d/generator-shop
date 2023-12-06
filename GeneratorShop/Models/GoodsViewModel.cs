namespace GeneratorShop.Models
{
    public class GoodsViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Price { get; set; }
        public decimal Power { get; set; }
        public int PowerOutput { get; set; }
        public string FuelConsuming { get; set; }
        public string Tank { get; set; }
        public string Weight { get; set; }
        public string TypeOfStart { get; set; }
  
        public GeneratorCategory Category { get; set; }
    }
    public class GeneratorCategory
    {
        public string Category { get; set; }

    }
}

