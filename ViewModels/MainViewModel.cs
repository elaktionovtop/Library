using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Library.Views;
using Library.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using Library.Migrations;
using System.Windows.Data;
using System.Windows.Controls;

namespace Library.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Library.Models.LoanRecord> _items;

        [ObservableProperty]
        private Library.Models.LoanRecord? _selectedItem;

        [ObservableProperty]
        private ObservableCollection<Reader> _readers;

        [ObservableProperty]
        private Reader? _selectedReader;

        [ObservableProperty]
        private ObservableCollection<BookCopy> _bookCopies;

        [ObservableProperty]
        private BookCopy? _selectedBookCopy;

        // конструктор получает книги и авторов
        public MainViewModel(IEnumerable<Library.Models.LoanRecord> items,
            IEnumerable<Reader> readers, 
            IEnumerable<BookCopy> bookCopies)
        {
            Items = new ObservableCollection<Library.Models.LoanRecord>(items);
            if(Items.Count > 0)
            {
                SelectedItem = Items[0];
                //UpdateBookAuthors();
            }
            Items.CollectionChanged += Items_CollectionChanged;

            Readers = new ObservableCollection<Reader>(readers);
            if(Readers.Count > 0)
            {
                SelectedReader = Readers[0];
            }

            BookCopies = new ObservableCollection<BookCopy>(bookCopies);
            if(BookCopies.Count > 0)
            {
                SelectedBookCopy = BookCopies[0];
            }

            //PropertyChanged += (s, e) =>
            //{
            //    if(e.PropertyName == nameof(SelectedItem))
            //    {
            //        UpdateBookAuthors();
            //    }
            //};
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

        [RelayCommand]
        public void BorrowBook()
        {
            if(SelectedReader is not null &&
                SelectedBookCopy is not null &&
                SelectedBookCopy.IsFree)
            {
                Items.Add(new Library.Models.LoanRecord()
                {
                    Reader = SelectedReader,
                    BookCopy = SelectedBookCopy,
                    LoanDate = DateOnly.FromDateTime(DateTime.Now)
                });
                SelectedBookCopy.IsFree = false;
                CollectionViewSource.GetDefaultView(BookCopies).Refresh();
                SelectedItem = Items.LastOrDefault();
            }
        }

        [RelayCommand]
        public void ReturnBook()
        {
            if(SelectedItem is not null  &&
                SelectedItem.ReturnDate is null)
            {
                SelectedItem.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
                SelectedBookCopy = BookCopies
                    .FirstOrDefault(bc => bc == SelectedItem.BookCopy);
                SelectedBookCopy.IsFree = true;
                CollectionViewSource.GetDefaultView(BookCopies).Refresh();
                CollectionViewSource.GetDefaultView(Items).Refresh();
            }
        }

        [RelayCommand]
        public void ViewReaders()
        {
            new ReadersWindow().ShowDialog();
            Readers = new ObservableCollection<Reader>(App.Repository.GetReaders());
        }

        [RelayCommand]
        public void ViewBookCopies()
        {
            new BookCopiesWindow().ShowDialog();
            BookCopies = new ObservableCollection<BookCopy>(App.Repository.GetBookCopies());
        }

        [RelayCommand]
        public void BookSearch()
        {
            foreach(Window window in Application.Current.Windows)
            {
                if(window.Title == "Поиск книг") return;
            }
            new BookSearchWindow().Show();
        }

        [RelayCommand]
        public void ViewLibrarians()
        {
            new LibrariansWindow().ShowDialog();
        }
    }
}
