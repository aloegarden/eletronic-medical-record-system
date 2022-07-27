using AppDev_Final_Project__Medical_Records_.objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDev_Final_Project__Medical_Records_.pages
{
    public class logCommands
    {
        string _connectionString = string.Empty;

        public logCommands()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        }

        public void InsertToPatientLogs(UserLogin user, Patient patient, string action)
        {
            string query = "INSERT INTO PatientLogstbl(patient_id, action_performed, performed_by, date_performed)  " +
                "VALUES(@patient_id, @action_perfomed, @performed_by, @date_performed)";

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@patient_id", patient.id);
                    command.Parameters.AddWithValue("@action_perfomed", action);
                    command.Parameters.AddWithValue("@performed_by", user.Id);
                    command.Parameters.AddWithValue("@date_performed", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertToConsultationLogs(UserLogin user, Consult consult, string action)
        {
            string action_performed = action;
            string query = "INSERT INTO ConsultationLogstbl(consultation_id, action_performed, performed_by, date_performed) " +
                "VALUES(@consultation_id, @action_performed, @performed_by, @date_performed)";

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@consultation_id", consult.id);
                    command.Parameters.AddWithValue("@action_performed", action_performed);
                    command.Parameters.AddWithValue("@performed_by", user.Id);
                    command.Parameters.AddWithValue("@date_performed", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertToLoginLogs(UserLogin userLogin)
        {
            string query = "INSERT INTO LoginLogstbl(user_id, logged_in_at, logged_out_at) " +
                "VALUES(@user_id, @logged_in_at, @logged_out_at)";

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@user_id", userLogin.Id);
                    command.Parameters.AddWithValue("@logged_in_at", userLogin.loginTime);
                    command.Parameters.AddWithValue("@logged_out_at", userLogin.logoutTime);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertMedicineToLogs(Medicine medicine, UserLogin userLogin, string action)
        {
            string query = "INSERT INTO MedicineLogstbl(action_performed, log, performed_by, date_performed) " + 
               "VALUES(@action_performed, @log, @performed_by, @date_performed)";

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@action_performed", action);
                    command.Parameters.AddWithValue("@log", medicine.medicine_name);
                    command.Parameters.AddWithValue("@performed_by", userLogin.Id);
                    command.Parameters.AddWithValue("@date_performed", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertReleasedMedicineToLogs(Release_Medicine releaseMed, UserLogin userLogin, string action)
        {
            string query = "INSERT INTO MedicineLogstbl(action_performed, log, performed_by, date_performed) " +
               "VALUES(@action_performed, @log, @performed_by, @date_performed)";


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@action_performed", action);
                    command.Parameters.AddWithValue("@log", releaseMed.medicine_name);
                    command.Parameters.AddWithValue("@performed_by", userLogin.Id);
                    command.Parameters.AddWithValue("@date_performed", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }

        }
    }
}
