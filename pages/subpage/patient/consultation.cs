using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using AppDev_Final_Project__Medical_Records_.objects;

namespace AppDev_Final_Project__Medical_Records_.pages.subpage.patient
{
    public class consultation
    {
        string _connectionString = string.Empty;

        public consultation()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        }

        public List<Consult> GetAllConsultations()
        {
            Consult consult = new Consult();
            List<Consult> consults = new List<Consult>();

            string query = "SELECT * FROM Consultationtbl";

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
                            consult = new Consult();
                            consult.id = Convert.ToInt32(reader["Id"]);
                            consult.patient_id = Convert.ToInt32(reader["patient_id"]);
                            consult.weight = (decimal)reader["weight"];
                            consult.height = (decimal)reader["height"];
                            consult.bp = reader["bp"].ToString();
                            consult.Bp_remarks = reader["bp_remarks"].ToString();
                            consult.temperature = (decimal)reader["temperature"];
                            consult.temperature_remarks = reader["temperature_remarks"].ToString();
                            consult.consultation_date = (DateTime)reader["consultation_date"];

                            consults.Add(consult);
                        }
                        reader.Close();
                        return consults;
                    }
                }
            }
            catch(Exception)
            {
                return null;
            }
        }

        public List<Consult> SearchConsultations(string searchText)
        {
            Consult consult = new Consult();
            List<Consult> consults = new List<Consult>();

            string query = "SELECT * FROM Consultationtbl WHERE Id LIKE '%" + searchText + "%' " + " OR patient_Id LIKE '%" + searchText + "%'";

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
                            consult = new Consult();
                            consult.id = Convert.ToInt32(reader["Id"]);
                            consult.patient_id = Convert.ToInt32(reader["patient_Id"]);
                            consult.weight = (decimal)reader["weight"];
                            consult.height = (decimal)reader["height"];
                            consult.bp = reader["bp"].ToString();
                            consult.Bp_remarks = reader["bp_remarks"].ToString();
                            consult.temperature = (decimal)reader["temperature"];
                            consult.temperature_remarks = reader["temperature_remarks"].ToString();
                            consult.consultation_date = (DateTime)reader["consultation_date"];
                        }
                        reader.Close();
                        return consults;
                    }
                }
            }
            catch(Exception)
            {
                return null;
            }
        }

        public int InsertConsultation(Consult consultationInfo)
        {
            string query = "INSERT INTO Consultationtbl(patient_Id, weight, height, bp, bp_remarks, temperature, temperature_remarks, consultation_date) " +
                "OUTPUT Inserted.Id VALUES(@patient_Id, @weight, @height, @bp, @bp_remarks, @temperature, @temperature_remarks, @consultation_date)";

            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@patient_Id", consultationInfo.patient_id);
                        command.Parameters.AddWithValue("@weight", consultationInfo.weight);
                        command.Parameters.AddWithValue("@height", consultationInfo.height);
                        command.Parameters.AddWithValue("@bp", consultationInfo.bp);
                        command.Parameters.AddWithValue("@bp_remarks", consultationInfo.Bp_remarks);
                        command.Parameters.AddWithValue("@temperature", consultationInfo.temperature);
                        command.Parameters.AddWithValue("@temperature_remarks", consultationInfo.temperature_remarks);
                        command.Parameters.AddWithValue("@consultation_date", consultationInfo.consultation_date);

                        int id = (int)command.ExecuteScalar();

                        return id;
                    }
                }
            }
            catch(Exception)
            {
                return 0;
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
                        while (reader.Read())
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
    }
}
