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
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;
using AppDev_Final_Project__Medical_Records_.objects;

namespace AppDev_Final_Project__Medical_Records_.pages
{
    /// <summary>
    /// Interaction logic for ManageUsers.xaml
    /// </summary>
    public partial class ManageUsers : Page
    {
        string _connectionString = string.Empty;
        public ManageUsers()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            InitializeComponent();
            PopulateTable();
        }

        private void PopulateTable()
        {
            User user = new User();
            List<User> users = new List<User>();

            string query = "SELECT * FROM Usertbl WHERE isAdmin = 0";

            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while(reader.Read())
                        {
                            user = new User();
                            user.Id = Convert.ToInt32(reader["Id"]);
                            user.fName = reader["fName"].ToString();
                            user.lName = reader["lName"].ToString();
                            user.age = Convert.ToInt32(reader["age"]);
                            user.contact = reader["contact"].ToString();
                            user.address = reader["address"].ToString();
                            user.dob = (DateTime)reader["dob"];
                            users.Add(user);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if(users != null)
            {
                foreach(User user1 in users)
                {
                    DataGridUsers.Items.Add(user1);
                }
            }
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private bool ProfileInputValid()
        {
            if (txtAddress.Text == null || txtAge.Text == null || txtContact.Text == null || txtFname.Text == null || txtLname.Text == null ||
                dateDOB.SelectedDate == null)
            {
                return false;
            }

            return true;
        }

        private void txtAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            string query = "UPDATE Usertbl SET fName=@fName, lName=@lName, age=@age, contact=@contact, address=@address, dob=@dob " +
                "WHERE Id=@Id";
            try
            {
                if (ProfileInputValid())
                {
                    User user = new User();
                    user.Id = Convert.ToInt32(txtID.Text);
                    user.fName = txtFname.Text.Trim().ToString();
                    user.lName = txtLname.Text.Trim().ToString();
                    user.contact = txtContact.Text.Trim().ToString();
                    user.address = txtAddress.Text.Trim().ToString();
                    user.age = Convert.ToInt32(txtAge.Text);
                    user.dob = (DateTime)dateDOB.SelectedDate;

                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
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
            catch (Exception)
            {
                MessageBox.Show("Do not leave any entries empty!");
            }
        }

        private void DataGridUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User user = DataGridUsers.SelectedItem as User;

            if(user != null)
            {
                txtID.Text = user.Id.ToString();
                txtFname.Text = user.fName.ToString();
                txtLname.Text = user.lName.ToString();
                txtAge.Text = user.age.ToString();
                txtContact.Text = user.contact.ToString();
                txtAddress.Text = user.address.ToString();
                dateDOB.SelectedDate = (DateTime)user.dob;
            }
        }
    }
}
