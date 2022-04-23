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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelAgency.App.Views
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Page
    {
        public ProfileWindow()
        {
            InitializeComponent();
        }

        private void ShowProfileInfo(object sender, RoutedEventArgs e)
        {
            var page = new ProfileInfo(); 
            ProfileFrame.Content = page;
        }

        private void ShowUserRides(object sender, RoutedEventArgs e)
        {
            var page = new UserRides();
            ProfileFrame.Content = page;
        }
    }
}
