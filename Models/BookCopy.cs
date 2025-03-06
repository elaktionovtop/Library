using Library.Migrations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookCopy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string RegNumber { get; set; }
        public bool IsFree { get; set; }

        public Book Book { get; set; }

        public override string ToString() => $"{Id}:{Book.Title}, {RegNumber}";
    }
}
