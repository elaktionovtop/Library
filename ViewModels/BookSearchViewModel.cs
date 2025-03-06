using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Library.Models;
using Library.Views;

namespace Library.ViewModels
{
    public partial class BookSearchViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<BookCopy> _items;

        [ObservableProperty]
        private BookCopy? _selectedItem;

        [ObservableProperty]
        private string _pattern;

        [RelayCommand]
        public void SearchByTitle()
        {
            var bookCopies = App.Repository.GetBookCopies()
                .Where(b => b.Book.Title.Contains(Pattern));
            Items = new ObservableCollection<BookCopy>(bookCopies);
        }

        [RelayCommand]
        public void SearchByAuthor()
        {
            var bookCopies = App.Repository.GetBookCopies()
                .Where(bc => bc.Book.Authors
                .Any(a => a.Name.Contains(Pattern)));
            Items = new ObservableCollection<BookCopy>(bookCopies);
        }
    }
}
