using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DeliveryAddress
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Order Order { get; set; }
    }
}
