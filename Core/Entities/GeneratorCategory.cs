using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GeneratorCategory
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public IEnumerable<Generator> Generators { get; set; }
    }
}
