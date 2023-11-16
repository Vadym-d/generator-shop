using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Generator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public double PowerOutput { get; set; }
        public double Weight { get; set; }
        public double FuelConsuming { get; set; }
        public double Tank { get; set; }
        public double Power { get; set; }
        public int GenratorCategoryId { get; set; }
        public GeneratorCategory GenratorCategory { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
