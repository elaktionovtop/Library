using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Library.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Views;

namespace Library.ViewModels
{
    public partial class BooksViewModel : ObservableObject
    {
        [RelayCommand]
        public void Authors()
        {
            new AuthorsWindow().ShowDialog();
        }
    }
}

