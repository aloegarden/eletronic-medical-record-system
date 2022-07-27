using AppDev_Final_Project__Medical_Records_.objects;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppDev_Final_Project__Medical_Records_
{
    public class Register
    {
        string _connectionString = string.Empty;

        public Register()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        }

        public string registerUser(User user)
        {
            if(!userExists(user))
            {
                string query = "INSERT INTO Usertbl(fName, lname, age, contact, address, dob, username, password, isAdmin) " +
                    "VALUES(@fName, @lName, @age, @contact, @address, @dob, @username, @password, @isAdmin)";

                try
                {
                    using(SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        using(SqlCommand comm = new SqlCommand(query, conn))
                        {
                            conn.Open();
                            comm.Parameters.AddWithValue("@fName", user.fName);
                            comm.Parameters.AddWithValue("@lName", user.lName);
                            comm.Parameters.AddWithValue("@age", user.age);
                            comm.Parameters.AddWithValue("@contact", user.contact);
                            comm.Parameters.AddWithValue("@address", user.address);
                            comm.Parameters.AddWithValue("@dob", user.dob.ToString("MM/dd/yyyy"));
                            comm.Parameters.AddWithValue("@username", user.username);
                            comm.Parameters.AddWithValue("@password", utils.hashPassword(user.password));
                            comm.Parameters.AddWithValue("@isAdmin", user.isAdmin);

                            comm.ExecuteNonQuery();

                            return "User added successfully";
                        }
                    }
                }
                catch(Exception ex)
                {
                    return "An error occured. Error Message: " + ex;
                }
            }

            return "Username already taken. Please try a different username.";
        }

        public bool userExists(User user)
        {

            string query = "SELECT COUNT(1) FROM Usertbl WHERE username=@Username";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand comm = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        comm.CommandType = CommandType.Text;
                        comm.Parameters.AddWithValue("@Username", user.username);
                        comm.Parameters.AddWithValue("@Password", utils.hashPassword(user.password));
                        int count = Convert.ToInt32(comm.ExecuteScalar());

                        if (count == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
