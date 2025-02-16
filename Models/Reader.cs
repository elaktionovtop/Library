using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }

        public override string ToString() => $"{Id}:{Name}, {BirthYear}";
    }
}
