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
    public partial class LibrariansViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Librarian> _items;

        [ObservableProperty]
        private Librarian _selectedItem;

        // конструктор получает данные из БД
        public LibrariansViewModel(IEnumerable<Librarian> items)
        {
            Items = new ObservableCollection<Librarian>(items);
            if(Items.Count > 0)
            {
                SelectedItem = Items[0];
            }
            Items.CollectionChanged += Items_CollectionChanged;
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
    }

}
