using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Library.Views;

namespace Library.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [RelayCommand]
        public void BorrowBook()
        {
            MessageBox.Show("Borrow Book");
        }

        [RelayCommand]
        public void ReturnBook()
        {
            MessageBox.Show("Return Book");
        }

        [RelayCommand]
        public void Readers()
        {
            new ReadersWindow().ShowDialog();
        }

        [RelayCommand]
        public void BookCopies()
        {
            MessageBox.Show("BookCopies");
        }

        [RelayCommand]
        public void Librarians()
        {
            MessageBox.Show("Librarians");
        }
    }
}
