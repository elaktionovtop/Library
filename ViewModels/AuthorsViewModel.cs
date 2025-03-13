using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

using Library.Models;

namespace Library.ViewModels
{
    public partial class AuthorsViewModel
    : ObservableCollectionViewModel<Author>
    {
        public AuthorsViewModel(IEnumerable<Author> items) : base(items) { }
    }
}
