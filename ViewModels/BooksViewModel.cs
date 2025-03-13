using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Library.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Views;
using Library.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Library.ViewModels
{
    public partial class BooksViewModel 
        : ObservableCollectionViewModel<Book>
    {
        //[ObservableProperty]
        //private ObservableCollection<Book> _items;

        //[ObservableProperty]
        //private Book? _selectedItem;

        [ObservableProperty]
        private ObservableCollection<Author> _allAuthors;

        [ObservableProperty]
        private Author? _selectedAuthor;

        [ObservableProperty]
        private ObservableCollection<Author> _bookAuthors = new();

        [ObservableProperty]
        private Author? _selectedBookAuthor;

        // конструктор получает книги и авторов
        public BooksViewModel(IEnumerable<Book> items,
            IEnumerable<Author> authors) :base(items)
        {
            //Items = new ObservableCollection<Book>(items);
            //if(Items.Count > 0)
            //{
            //    SelectedItem = Items[0];
            //    UpdateBookAuthors();
            //}
            //Items.CollectionChanged += Items_CollectionChanged;
            
            AllAuthors = new ObservableCollection<Author>(authors);
            if(AllAuthors.Count > 0)
            {
                SelectedAuthor = AllAuthors[0];
            }

            PropertyChanged += (s, e) =>
            {
                if(e.PropertyName == nameof(SelectedItem))
                {
                    UpdateBookAuthors();
                }
            };
        }

        //// добавления и удаления Items передаем в репозиторий БД
        //public void Items_CollectionChanged(object? sender,
        //        NotifyCollectionChangedEventArgs e)
        //{
        //    Debug.WriteLine(e.Action);
        //    switch(e.Action)
        //    {
        //        case NotifyCollectionChangedAction.Add:
        //            if(e.NewItems is not null)
        //            {
        //                foreach(var item in e.NewItems)
        //                {
        //                    App.Repository.Add(item);
        //                }
        //            }
        //            break;
        //        case NotifyCollectionChangedAction.Remove:
        //            if(e.OldItems is not null)
        //            {
        //                foreach(var item in e.OldItems)
        //                {
        //                    App.Repository.Remove(item);
        //                }
        //            }
        //            break;
        //    }
        //}

        private void UpdateBookAuthors()
        {
            BookAuthors.Clear();
            if(SelectedItem is not null)
            {
                foreach(var author in SelectedItem.Authors)
                {
                    BookAuthors.Add(author);
                }
                if(BookAuthors.Count > 0)
                {
                    SelectedBookAuthor = BookAuthors[0];
                }
            }
        }

        [RelayCommand]
        public void Authors()
        {
            new AuthorsWindow().ShowDialog();
            AllAuthors = new ObservableCollection<Author>(App.Repository.GetAuthors());
        }

        [RelayCommand]
        public void AddAuthor()
        {
            if(SelectedItem is null || SelectedAuthor is null) return;

            if(!SelectedItem.Authors.Contains(SelectedAuthor))
            {
                SelectedItem.Authors.Add(SelectedAuthor);
                BookAuthors.Add(SelectedAuthor);
            }
        }

        [RelayCommand]
        public void RemoveAuthor()
        {
            if(SelectedItem is null || SelectedBookAuthor is null) return;

            if(SelectedItem.Authors.Contains(SelectedBookAuthor))
            {
                SelectedItem.Authors.Remove(SelectedBookAuthor);
                BookAuthors.Remove(SelectedBookAuthor);
            }
        }
    }
}

