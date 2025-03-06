using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Library.Models;
using Library.Views;

namespace Library.ViewModels
{
    public partial class BookCopiesViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<BookCopy> _items;

        [ObservableProperty]
        private BookCopy? _selectedItem;

        [ObservableProperty]
        private ObservableCollection<Book> _books;

        [ObservableProperty]
        private Book? _selectedBook;

        [ObservableProperty]
        private string _regNumber;

        // конструктор получает книги и авторов
        public BookCopiesViewModel(IEnumerable<BookCopy> items,
            IEnumerable<Book> books)
        {
            Items = new ObservableCollection<BookCopy>(items);
            if(Items.Count > 0)
            {
                SelectedItem = Items[0];
                UpdateBook();
            }
            Items.CollectionChanged += Items_CollectionChanged;

            Books = new ObservableCollection<Book>(books);
            if(Books.Count > 0)
            {
                SelectedBook = Books[0];
            }

            PropertyChanged += (s, e) =>
            {
                if(e.PropertyName == nameof(SelectedItem))
                {
                    UpdateBook();
                }
            };
        }
        // добавления и удаления Items передаем в репозиторий БД
        public void Items_CollectionChanged(object? sender,
                NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine(e.Action);
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if(e.NewItems is not null)
                    {
                        foreach(var item in e.NewItems)
                        {
                            App.Repository.Add(item);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if(e.OldItems is not null)
                    {
                        foreach(var item in e.OldItems)
                        {
                            App.Repository.Remove(item);
                        }
                    }
                    break;
            }
        }

        private void UpdateBook()
        {
            if(SelectedItem is not null)
            {
                RegNumber = SelectedItem.RegNumber;
            }
        }

        [RelayCommand]
        public void AddBookCopy()
        {
            if (SelectedBook is not null &&
                !string.IsNullOrEmpty(RegNumber))
                {
                    Items.Add(new BookCopy()
                    {
                        RegNumber = RegNumber,
                        Book = SelectedBook
                    });
                }
        }

        [RelayCommand]
        public void ViewBooks()
        {
            new BooksWindow().ShowDialog();
            Books = new ObservableCollection<Book>(App.Repository.GetBooks());
        }
    }
}
