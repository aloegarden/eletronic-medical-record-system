using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using AppDev_Final_Project__Medical_Records_.objects;
using System.Data.SqlClient;

namespace AppDev_Final_Project__Medical_Records_.pages.subpage.medicine
{
    public class medicineSQLCommands
    {
        string _connectionString = string.Empty;

        public medicineSQLCommands()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        }

        public List<Medicine> GetMedicine(string search) 
        {
            Medicine medicine = new Medicine();
            List<Medicine> medicines = new List<Medicine>();

            string query = "SELECT * FROM Medicinetbl WHERE medicine_name LIKE '%" + search + "%'";

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
                            medicine = new Medicine();
                            medicine.Id = (int)reader["Id"];
                            medicine.medicine_name = reader["medicine_name"].ToString();
                            medicine.description = reader["description"].ToString();
                            medicine.quantity = (int)reader["quantity"];
                            medicines.Add(medicine);
                        }
                        reader.Close();
                    }
                }

                return medicines;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public List<Medicine> GetMedicines() 
        {
            Medicine medicine = new Medicine();
            List<Medicine> medicines = new List<Medicine>();

            string query = "SELECT * FROM Medicinetbl";

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
                            medicine = new Medicine();
                            medicine.Id = (int)reader["Id"];
                            medicine.medicine_name = reader["medicine_name"].ToString();
                            medicine.description = reader["description"].ToString();
                            medicine.quantity = (int)reader["quantity"];
                            medicines.Add(medicine);
                        }
                        reader.Close();
                    }
                }

                return medicines;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public string InsertMedicine(Medicine medicine) 
        {
            string query = "INSERT INTO Medicinetbl(medicine_name, description, quantity) VALUES(@medicine_name, @description, @quantity)";

            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@medicine_name", medicine.medicine_name.Trim().ToString());
                        command.Parameters.AddWithValue("@description", medicine.description.Trim().ToString());
                        command.Parameters.AddWithValue("@quantity", medicine.quantity);
                        command.ExecuteNonQuery();
                       

                    }
                }
                 return "Added Medicine";
                
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }
        }

        public string UpdateMedicine(Medicine medicine)  
        {
            string query = "UPDATE Medicinetbl SET medicine_name=@medicine_name, description=@description, quantity=@quantity WHERE Id=@Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Id", medicine.Id);
                        command.Parameters.AddWithValue("@medicine_name", medicine.medicine_name.Trim().ToString());
                        command.Parameters.AddWithValue("@description", medicine.description.Trim().ToString());
                        command.Parameters.AddWithValue("@quantity", medicine.quantity);
                        command.ExecuteNonQuery();
                    }
                }

                return "Updated Medicine";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string DeleteMedicine(Medicine medicine) 
        {
            string query = "DELETE FROM Medicinetbl WHERE Id=@Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Id",medicine.Id);
                        command.ExecuteNonQuery();
                    }
                }

                return "Deleted Medicine";
            }
            catch (Exception ex)
            {
                return null;
            
            }
        }

        public string ReleaseMedicine(Release_Medicine releaseMed)
        {
          
            string query = "INSERT INTO ReleasedMedicinetbl(patient_id, medicine_id, quantity, reason, date_released) VALUES(@patient_id, @medicine_id, @quantity, @reason, @date_released)";

            try
            {
                using (SqlConnection connection =new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();          
                        command.Parameters.AddWithValue("@patient_id", releaseMed.patient_id);
                        command.Parameters.AddWithValue("@medicine_id", releaseMed.Id);
                        command.Parameters.AddWithValue("@quantity", releaseMed.quantity);
                        command.Parameters.AddWithValue("@reason", releaseMed.reason);
                        command.Parameters.AddWithValue("@date_released",DateTime.Now);
                        command.ExecuteNonQuery();
                    }
                }
                return "Release Medicine";                   
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Release_Medicine> GetReleaseMedicines()
        {
            Release_Medicine releaseMed = new Release_Medicine();
            List<Release_Medicine> ReleaseMedicines = new List<Release_Medicine>();

            string query = "SELECT * FROM ReleasedMedicinetbl";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            releaseMed = new Release_Medicine();

                            releaseMed.id = Convert.ToInt32(reader["Id"]);
                            releaseMed.patient_id = Convert.ToInt32(reader["patient_id"]);
                            releaseMed.Id = Convert.ToInt32(reader["medicine_id"]);
                            releaseMed.quantity = Convert.ToInt32(reader["quantity"]);
                            releaseMed.reason = reader["reason"].ToString();
                            releaseMed.date_released = (DateTime)reader["date_released"];

                            ReleaseMedicines.Add(releaseMed);

                            
                        }
                         reader.Close();
                        return ReleaseMedicines;
                    }   

                    
                }
            }
            catch (Exception)
            {
                return null;
            }
  
        }

        public string UpdateReleaseMedicine(Release_Medicine releaseMed)
        {
            string query = "UPDATE Medicinetbl SET quantity = quantity - @quantity WHERE Id=@Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Id", releaseMed.Id);
                        command.Parameters.AddWithValue("@quantity", releaseMed.quantity);
                        command.ExecuteNonQuery();
                    }
                }

                return "Stocks updated";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string CheckMedQuantity(Release_Medicine releaseMed)
        {
            
        
            string query = "SELECT Id, quantity FROM Medicinetbl WHERE Id=@Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Id", releaseMed.Id);
                        

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            releaseMed.Id = (int)reader["Id"];
                            releaseMed.quantity = (int)reader["quantity"];
                            
                        }
                        reader.Close();
                        command.ExecuteNonQuery();
                    }
                }
                return (releaseMed.quantity.ToString());

            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public bool checksId(Release_Medicine releaseMed)
        {
            bool valid = false;
            string query = "SELECT Id FROM Patienttbl WHERE Id=@Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Id", releaseMed.patient_id);

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if(releaseMed.patient_id == (int)reader["Id"])
                            {   
                                valid = true;
                            }
                        }
                        reader.Close();
                        command.ExecuteNonQuery();

                    }
                }
                return valid;
            }
            catch(Exception ex)
            {
                valid = false;
                return valid;

            }
        }


    }
    
}
