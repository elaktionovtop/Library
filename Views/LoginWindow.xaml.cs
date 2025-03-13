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
        int _maxRepetitionCount;
        IEnumerable<Librarian> _librarians;

        public LoginWindow(IEnumerable<Librarian> librarians, 
            int maxRepetitionCount = 3)
        {
            InitializeComponent();
            _librarians = librarians;
            _maxRepetitionCount = maxRepetitionCount;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if(string.IsNullOrWhiteSpace(username))
            {
                App.UserType = UserType.Reader;
                DialogResult = true;
                return;
            }

            var user = _librarians.FirstOrDefault(l => l.Login == username);
            if (user?.Login == "Admin")
            {
                App.UserType = UserType.Admin;
            }
            else
            {
                App.UserType = UserType.Librarian;
            }

            if(user?.Password == password)
            {
                DialogResult = true;
            }
            else
            {
                if(--_maxRepetitionCount == 0)
                {
                    MessageBox.Show("Неверный логин или пароль\nЧисло попыток исчерпано.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    DialogResult = false;
                }
                else
                {
                    MessageBox.Show($"Неверный логин или пароль\n" +
                        $"У Вас еще попыток: {_maxRepetitionCount}",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
