using AppDev_Final_Project__Medical_Records_.objects;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppDev_Final_Project__Medical_Records_.pages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private UserLogin user;
        string _connectionString = string.Empty;

        public ProfilePage(UserLogin user)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            this.user = user;
            InitializeComponent();
            PopulateFields();
        }

        private void PopulateFields()
        {
            GetUser();
            txtAddress.Text = user.address;
            txtAge.Text = user.age.ToString();
            txtContact.Text = user.contact;
            txtFname.Text = user.fName;
            txtLname.Text = user.lName;
            txtUsername.Text = user.username;
            dateDOB.SelectedDate = user.dob;
        }

        private void GetUser()
        {
            string query = "SELECT * FROM Usertbl WHERE Id=@Id";

            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Id", user.Id);
                        SqlDataReader reader = command.ExecuteReader();
                        while(reader.Read())
                        {
                            user.address = reader["address"].ToString();
                            user.dob = (DateTime)reader["dob"];
                            user.age = (int)reader["age"];
                            user.contact = reader["contact"].ToString();
                            user.fName = reader["fName"].ToString();
                            user.lName = reader["lName"].ToString();
                            user.username = reader["username"].ToString();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ProfileInputValid()
        {
            if((txtAddress.Text == null || txtAge.Text == null || txtContact.Text == null || txtFname.Text == null || txtLname.Text == null ||
                dateDOB.SelectedDate == null) || (txtPassword.Password == null || txtPasswordAgain.Password == null))
            {
                return false;
            }

            return true;
        }

        private bool PasswordsMatch()
        {
            if(utils.hashPassword(txtPassword.Password) != utils.hashPassword(txtPasswordAgain.Password))
            {
                return false;
            }
            return true;
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            string query = "UPDATE Usertbl SET fName=@fName, lName=@lName, age=@age, contact=@contact, address=@address, dob=@dob " +
                "WHERE Id=@Id";
            try
            {
                if(ProfileInputValid())
                {
                    user.fName = txtFname.Text.Trim().ToString();
                    user.lName = txtLname.Text.Trim().ToString();
                    user.contact = txtContact.Text.Trim().ToString();
                    user.address = txtAddress.Text.Trim().ToString();
                    user.age = Convert.ToInt32(txtAge.Text);
                    user.dob = (DateTime)dateDOB.SelectedDate;

                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        using(SqlCommand command = new SqlCommand(query, connection))
                        {
                            connection.Open();
                            command.Parameters.AddWithValue("@fName", user.fName);
                            command.Parameters.AddWithValue("@lName", user.lName);
                            command.Parameters.AddWithValue("@age", user.age);
                            command.Parameters.AddWithValue("@contact", user.contact);
                            command.Parameters.AddWithValue("@address", user.address);
                            command.Parameters.AddWithValue("@dob", user.dob);
                            command.Parameters.AddWithValue("@Id", user.Id);
                            command.ExecuteNonQuery();

                            MessageBox.Show("Profile Updated!");
                        }
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Do not leave any entries empty!");
            }
        }

        private void btnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            string query = "UPDATE Usertbl SET password=@password WHERE Id=@Id";

            try
            {
                if(ProfileInputValid() && PasswordsMatch())
                {
                    user.password = utils.hashPassword(txtPasswordAgain.Password);
                    using(SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        using(SqlCommand command = new SqlCommand(query, connection))
                        {
                            connection.Open();
                            command.Parameters.AddWithValue("@password", user.password);
                            command.Parameters.AddWithValue("@Id", user.Id);
                            command.ExecuteNonQuery();

                            MessageBox.Show("Password Updated!");
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
