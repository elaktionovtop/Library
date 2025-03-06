using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library.Views
{
    /// <summary>
    /// Логика взаимодействия для LibrariansWindow.xaml
    /// </summary>
    public partial class LibrariansWindow : Window
    {
        public LibrariansWindow()
        {
            InitializeComponent();
            DataContext = App.LibrariansViewModel;
            Closed += (s, e) => App.Repository.SaveChanges();
        }
    }
}
