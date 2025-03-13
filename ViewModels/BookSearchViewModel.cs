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
        private ObservableCollection<Author> _bookAuthors = new();

        [ObservableProperty]
        private string _pattern;

        public BookSearchViewModel()
        {
            PropertyChanged += (s, e) =>
            {
                if(e.PropertyName == nameof(SelectedItem))
                {
                    UpdateBookAuthors();
                }
            };
        }

        [RelayCommand]
        public void SearchByTitle()
        {
            var bookCopies = App.Repository.GetBookCopies()
                .Where(b => b.Book.Title.ToUpper().Contains(Pattern.ToUpper()));
            Items = new ObservableCollection<BookCopy>(bookCopies);
            if(Items.Count > 0)
            {
                SelectedItem = Items[0];
            }

        }

        [RelayCommand]
        public void SearchByAuthor()
        {
            var bookCopies = App.Repository.GetBookCopies()
                .Where(bc => bc.Book.Authors
                .Any(a => a.Name.ToUpper().Contains(Pattern.ToUpper())));
            Items = new ObservableCollection<BookCopy>(bookCopies);
            if(Items.Count > 0)
            {
                SelectedItem = Items[0];
            }

        }

        private void UpdateBookAuthors()
        {
            BookAuthors.Clear();
            if(SelectedItem is not null)
            {
                foreach(var author in SelectedItem.Book.Authors)
                {
                    BookAuthors.Add(author);
                }
            }
        }
    }
}
