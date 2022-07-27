using AppDev_Final_Project__Medical_Records_.objects;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace AppDev_Final_Project__Medical_Records_
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validInputs())
                {
                    User user = new User();
                    user.fName = txtfName.Text.Trim().ToString();
                    user.lName = txtlName.Text.Trim().ToString();
                    user.age = Convert.ToInt32(txtAge.Text);
                    user.contact = txtContact.Text.Trim().ToString();
                    user.address = txtAddress.Text.Trim().ToString();
                    user.dob = dateDOB.SelectedDate.Value;
                    user.username = txtUsername.Text.Trim().ToString();
                    user.password = txtPassword.Password;

                    if (acctype.SelectedValue.ToString() == "Admin")
                    {
                        user.isAdmin = true;
                    }
                    else
                    {
                        user.isAdmin = false;
                    }

                    Register register = new Register();
                    string result = register.registerUser(user);

                    MessageBox.Show(result);

                    if (result == "User added successfully")
                    {
                        txtfName.Clear();
                        txtlName.Clear();
                        txtAge.Clear();
                        txtContact.Clear();
                        txtAddress.Clear();
                        dateDOB.SelectedDate = null;
                        txtUsername.Clear();
                        txtPassword.Clear();
                        acctype.Items.Clear();
                    }                  
                }
                else
                {
                    MessageBox.Show("Please do not leave any fields blank");
                    return;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Please do not leave any fields blank");
                return;
            }
            
        }

        private bool validInputs()
        {
            if(txtfName.Text == null || txtfName.Text == null || txtAge.Text == null || txtContact.Text == null ||
                txtAddress.Text == null || dateDOB.Text.ToString() == null || txtUsername.Text == null || txtPassword.Password == null ||
                acctype.SelectedIndex < 0)
            {
                return false;
            }

            return true;
        }

        private void txtAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
