using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int GeneratorId { get; set; }
        public Generator Generator { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
