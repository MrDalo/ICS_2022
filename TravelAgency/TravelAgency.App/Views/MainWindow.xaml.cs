using System.Windows;
using System.Windows.Controls;
using TravelAgency.App.ViewModels;

namespace TravelAgency.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void profilview(object sender, RoutedEventArgs e)
        {
            var page = new ProfileWindow();
            MainFrame.Content = page;
        }
    }
}
