using AppDev_Final_Project__Medical_Records_.objects;
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

namespace AppDev_Final_Project__Medical_Records_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (validInputs())
            {
                UserLogin user = new UserLogin();
                user.username = txtUsername.Text.Trim().ToString();
                user.password = txtPassword.Password;

                Login login = new Login();
                int accExists = login.userExists(user);
                bool isAdmin = login.isAdmin(user);

                if (accExists > 0)
                {
                    user.Id = accExists;
                    user.loginTime = DateTime.Now;
                    user.isAdmin = isAdmin;
                    if(user.isAdmin)
                    {
                        AdminPanel adminPanel = new AdminPanel(user);
                        adminPanel.Show();
                    }
                    else
                    {
                        UserPanel userPanel = new UserPanel(user);
                        userPanel.Show();
                    }
                    
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid Credentials. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Username/Password fields are empty.");
                return;
            }
            
        }

        private bool validInputs()
        {
            if(txtUsername.Text == null || txtPassword.Password == null)
            {
                return false;
            }

            return true;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }
    }
}
