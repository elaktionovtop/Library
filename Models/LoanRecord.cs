using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class LoanRecord
    {
        public int Id { get; set; }
        public int ReaderId { get; set; }
        public int BookCopyId { get; set; }

        public DateOnly LoanDate { get; set; }
        public DateOnly? ReturnDate { get; set; }

        public Reader Reader { get; set; }
        public BookCopy BookCopy { get; set; }

        public override string ToString() =>
            $"{Reader?.Name} {BookCopy?.Book?.Title} {LoanDate} {ReturnDate}";
    }
}
