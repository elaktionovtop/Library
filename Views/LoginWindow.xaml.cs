using Library.Models;

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
    public partial class LoginWindow : Window
    {
        IEnumerable<Librarian> _librarians;
        public UserType UserType { get; set; } = UserType.Librarian;

        public LoginWindow(IEnumerable<Librarian> librarians)
        {
            InitializeComponent();
            _librarians = librarians;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            var user = _librarians.FirstOrDefault(l => l.Login == username);
            if (user.Login == "Admin")
            {
                UserType = UserType.Admin;
            }

            if (user is not null && user.Password == password)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
            }
        }
    }
}
