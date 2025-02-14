using Library.Data;
using Library.ViewModels;

using System.Configuration;
using System.Data;
using System.Windows;

using static Library.Data.DbLibraryContext;

namespace Library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static DbLibraryContext? _repository;
        public static DbLibraryContext Repository => _repository;

        private static MainViewModel? _mainViewModel;
        public static MainViewModel MainViewModel => _mainViewModel;

        private static ReadersViewModel? _readerViewModel;
        public static ReadersViewModel ReadersViewModel => _readerViewModel;

        void App_StartUp(object sender, EventArgs e)
        {
            try
            {
                _repository = new ContextFactory().
                    CreateDbContext(Array.Empty<string>());
                _repository.Load();

                _mainViewModel = new MainViewModel();
                _readerViewModel = new ReadersViewModel
                    (_repository.GetReaders());
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
