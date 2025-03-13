using Library.Data;
using Library.Models;
using Library.ViewModels;
using Library.Views;

using System.Configuration;
using System.Data;
using System.Windows;

using static Library.Data.DbLibraryContext;

namespace Library
{
    public partial class App : Application
    {
        public static UserType UserType { get; set; }

        private static DbLibraryContext? _repository;
        public static DbLibraryContext Repository => _repository;

        private static MainViewModel? _mainViewModel;
        public static MainViewModel MainViewModel => _mainViewModel;

        private static ReadersViewModel? _readersViewModel;
        public static ReadersViewModel ReadersViewModel => _readersViewModel;

        private static BookCopiesViewModel? _bookCopiesViewModel;
        public static BookCopiesViewModel BookCopiesViewModel => _bookCopiesViewModel;

        private static BooksViewModel? _booksViewModel;
        public static BooksViewModel BooksViewModel =>
        _booksViewModel; 
        
        private static AuthorsViewModel? _authorsViewModel;
        public static AuthorsViewModel AuthorsViewModel => _authorsViewModel;

        private static LibrariansViewModel? _librariansViewModel;
        public static LibrariansViewModel LibrariansViewModel => _librariansViewModel;

        private static BookSearchViewModel? _bookSearchViewModel;
        public static BookSearchViewModel BookSearchViewModel =>
        _bookSearchViewModel;

        void App_StartUp(object sender, EventArgs e)
        {
            try
            {
                _repository = new ContextFactory().
                    CreateDbContext(Array.Empty<string>());
                _repository.Load();

                _mainViewModel = new MainViewModel(_repository.GetLoanRecords(),
                    _repository.GetReaders(), _repository.GetBookCopies());
                _readersViewModel = new ReadersViewModel
                    (_repository.GetReaders());
                _booksViewModel = new BooksViewModel
                    (_repository.GetBooks(), _repository.GetAuthors());
                _bookCopiesViewModel = new BookCopiesViewModel(_repository.GetBookCopies(), _repository.GetBooks());
                _authorsViewModel = new AuthorsViewModel
                    (_repository.GetAuthors());
                _librariansViewModel = new LibrariansViewModel(_repository.GetLibrarians());
                _bookSearchViewModel = new BookSearchViewModel();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"DB error!\n {ex.Message}");
                App.Current.Shutdown();
            }
        }

        public void App_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                _repository?.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"DB error!\n {ex.Message}");
            }
        }
    }
}
