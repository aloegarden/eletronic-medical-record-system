using AppDev_Final_Project__Medical_Records_.objects;
using AppDev_Final_Project__Medical_Records_.pages;
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

namespace AppDev_Final_Project__Medical_Records_
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        logCommands logCommands = new logCommands();
        private UserLogin user;

        public AdminPanel(UserLogin user)
        {
            InitializeComponent();

            Main.Content = new HomePage(user);
            this.user = user;
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPatient_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientPage(user);
        }

        private void btnMedicine_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MedicinePage(user);
        }

        private void btnLogs_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new LogPage();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            user.logoutTime = DateTime.Now;
            logCommands.InsertToLoginLogs(user);
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProfilePage(user);
        }

        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ManageUsers();
        }
    }
}
