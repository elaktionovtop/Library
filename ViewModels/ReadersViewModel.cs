using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

using Library.Models;

namespace Library.ViewModels
{
    public partial class ReadersViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Person> _items;

        [ObservableProperty]
        private Person _selectedItem;

        // конструктор получает данные из БД
        public ReadersViewModel(IEnumerable<Person> items)
        {
            Items = new ObservableCollection<Person>(items);
            if(Items.Count > 0)
            {
                SelectedItem = Items[0];
            }
            Items.CollectionChanged += Items_CollectionChanged;
        }

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
