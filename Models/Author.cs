using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new();

        public override string ToString()
        {
            string result = $"{Id}:{Name}";
            foreach(var book in Books)
            {
                result += " | " + book.Title;
            }
            return result;
        }
    }
}
