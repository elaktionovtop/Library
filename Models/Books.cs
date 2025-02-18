using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Author> Authors { get; set; } = new();

        public override string ToString()
        {
            string result = $"{Title} {Authors?[0].Name ?? "Неизвестен"} ";
            foreach(var author in Authors)
            {
                result += " " + author.Name;
            }
            return result;
        }
    }
}
