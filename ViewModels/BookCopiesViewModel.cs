using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Library.Views;

namespace Library.ViewModels
{
    public partial class BookCopiesViewModel : ObservableObject
    {
        [RelayCommand]
        public void Books()
        {
            new BooksWindow().ShowDialog();
        }
    }
}
