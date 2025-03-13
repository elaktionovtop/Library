using CommunityToolkit.Mvvm.ComponentModel;
using Library.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public partial class LibrariansViewModel 
        : ObservableCollectionViewModel<Librarian>
    {
        public LibrariansViewModel(IEnumerable<Librarian> items) 
            : base(items) { }
    }
}
