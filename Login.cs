using AppDev_Final_Project__Medical_Records_.objects;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AppDev_Final_Project__Medical_Records_
{
    public class Login
    {
        string _connectionString = string.Empty;
        public Login()
        {
            
            _connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        }

        public bool isAdmin(User user)
        {
            string query = "SELECT isAdmin FROM Usertbl WHERE username=@username AND password=@password";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand comm = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        comm.Parameters.AddWithValue("@username", user.username);
                        comm.Parameters.AddWithValue("@password", utils.hashPassword(user.password));

                        bool isAdmin = new bool();
                        SqlDataReader reader = comm.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            isAdmin = (bool)reader["isAdmin"];  
                        }
                        return isAdmin;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int userExists(User user)
        {
            
            string query = "SELECT Id FROM Usertbl WHERE username=@username AND password=@password";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand comm = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        comm.Parameters.AddWithValue("@username", user.username);
                        comm.Parameters.AddWithValue("@password", utils.hashPassword(user.password));

                        int id = new int();
                        SqlDataReader reader = comm.ExecuteReader();

                        if(reader.HasRows)
                        {
                            reader.Read();
                            id = Convert.ToInt32(reader["Id"]);
                            return id;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            catch(Exception)
            {
                return 0;
            }

        }
    }
}
