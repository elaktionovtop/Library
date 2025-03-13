using Library.Models;
using Library.Views;

using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Library
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoginWindow loginWindow = new LoginWindow
            (App.Repository.GetLibrarians(), 1);

            if(loginWindow.ShowDialog() != true)
            {
                App.Current.Shutdown();
            }

            InitializeComponent();
            DataContext = App.MainViewModel;
            switch (App.UserType)
            {
                case UserType.Librarian:
                    LibrariansCommand.Visibility = Visibility.Hidden;
                    break;
                case UserType.Reader:
                    App.MainViewModel.BookSearchCommand.Execute(null);
                    this.Close();
                    break;
            }
        }

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid grid = sender as DataGrid;
            grid?.ScrollIntoView(grid?.SelectedItem);
        }
    }
}


