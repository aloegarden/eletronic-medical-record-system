using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using AppDev_Final_Project__Medical_Records_.objects;

namespace AppDev_Final_Project__Medical_Records_.pages.subpage.patient
{
    public class patient
    {
        string _connectionString = string.Empty;
        public patient()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        }

        public int InsertPatient(Patient patient)
        {
            string query = "INSERT INTO Patienttbl(fname, lname, gender, age, contact, address, dob)" +
                " OUTPUT Inserted.Id VALUES(@fname, @lname, @gender, @age, @contact, @address, @dob)";

            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@fName", patient.fname);
                        command.Parameters.AddWithValue("@lName", patient.lname);
                        command.Parameters.AddWithValue("@age", patient.age);
                        command.Parameters.AddWithValue("@gender", patient.gender);
                        command.Parameters.AddWithValue("@contact", patient.contact);
                        command.Parameters.AddWithValue("@address", patient.address);
                        command.Parameters.AddWithValue("@dob", patient.dob.ToString("MM/dd/yyyy"));

                        int id = (int)command.ExecuteScalar();
                        return id;
                    }
                }

            }catch(Exception)
            {
                return 0;
            }
        }

        public string UpdatePatient(Patient patient)
        {
            string query = "UPDATE Patienttbl SET fname=@fname, lname=@lname, gender=@gender, age=@age, " +
                "contact=@contact, address=@address, dob=@dob WHERE Id=@Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Id", patient.id);
                        command.Parameters.AddWithValue("@fname", patient.fname);
                        command.Parameters.AddWithValue("@lname", patient.lname);
                        command.Parameters.AddWithValue("@gender", patient.gender);
                        command.Parameters.AddWithValue("@age", patient.age);
                        command.Parameters.AddWithValue("@contact", patient.contact);
                        command.Parameters.AddWithValue("@address", patient.address);
                        command.Parameters.AddWithValue("@dob", patient.dob);

                        command.ExecuteNonQuery();

                        return "Patient data updated.";
                    }
                }

            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeletePatient(Patient patient)
        {
            string query = "DELETE FROM Patienttbl WHERE Id=@Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Id", patient.id);
                        command.ExecuteNonQuery();

                        return "Patient data deleted.";
                    }
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Patient> SearchPatient(string searchText)
        {
            Patient patient = new Patient();
            List<Patient> patients = new List<Patient>();
            string query = "SELECT * FROM Patienttbl WHERE fname LIKE '%" + searchText + "%' OR lname LIKE '%" + searchText + "%' " +
                "OR CONCAT(fname, ' ', lname) LIKE '%" + searchText + "%'";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand comm = new SqlCommand(query, conn))
                    {
                        conn.Open();

                        SqlDataReader reader = comm.ExecuteReader();
                        while(reader.Read())
                        {
                            patient = new Patient();
                            patient.id = Convert.ToInt32(reader["Id"]);
                            patient.fname = reader["fname"].ToString();
                            patient.lname = reader["lname"].ToString();
                            patient.gender = reader["gender"].ToString();
                            patient.age = (int)reader["age"];
                            patient.contact = reader["contact"].ToString();
                            patient.address = reader["address"].ToString();
                            patient.dob = (DateTime)reader["dob"];

                            patients.Add(patient);
                        }
                        reader.Close();
                        return patients;
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Patient> GetAllPatients()
        {
            Patient patient = new Patient();
            List<Patient> patients = new List<Patient>();

            string query = "SELECT * FROM Patienttbl";

            try
            {
                using(SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using(SqlCommand comm = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        SqlDataReader reader = comm.ExecuteReader();
                        while(reader.Read())
                        {
                            patient = new Patient();
                            patient.id = Convert.ToInt32(reader["Id"]);
                            patient.fname = reader["fname"].ToString();
                            patient.lname = reader["lname"].ToString();
                            patient.gender = reader["gender"].ToString();
                            patient.age = (int)reader["age"];
                            patient.contact = reader["contact"].ToString();
                            patient.address = reader["address"].ToString();
                            patient.dob = (DateTime)reader["dob"];

                            patients.Add(patient);
                        }
                        reader.Close();
                        return patients;
                    }
                }

            }catch(Exception)
            {
                return null;
            }
        }

    }
}
