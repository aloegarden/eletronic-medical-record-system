using AppDev_Final_Project__Medical_Records_.objects;
using AppDev_Final_Project__Medical_Records_.pages.subpage.medicine;
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

namespace AppDev_Final_Project__Medical_Records_.pages
{
    /// <summary>
    /// Interaction logic for MedicinePage.xaml
    /// </summary>
    public partial class MedicinePage : Page
    {
        private UserLogin user;

        public MedicinePage(UserLogin user)
        {
            this.user = user;
            InitializeComponent();
            Main.Content = new ManageMedicine(user);
        }
        private void btnManageMedicine_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ManageMedicine(user);
        }

        private void btnReleaseMedicine_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ReleaseMedicine(user);
        }

        private void btnViewReleasedMedicine_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ViewReleasedMedicine();
        }
    }
}
