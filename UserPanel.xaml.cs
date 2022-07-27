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
    /// Interaction logic for UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        logCommands logCommands = new logCommands();
        private UserLogin user;

        public UserPanel(UserLogin user)
        {
            this.user = user;
            InitializeComponent();
            Main.Content = new HomePage(user);
        }

        private void btnPatient_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PatientPage(user);
        }

        private void btnMedicine_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MedicinePage(user);
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ProfilePage(user);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            user.logoutTime = DateTime.Now;
            logCommands.InsertToLoginLogs(user);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
