using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
