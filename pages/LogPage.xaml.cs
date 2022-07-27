using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace AppDev_Final_Project__Medical_Records_.pages
{
    public partial class LogPage : Page
    {
        string _connectionString = string.Empty;
        public LogPage()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

            InitializeComponent();
            DataContext = new ComboBoxItems();
        }

        private void logList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(logList.SelectedItem.ToString())
            {
                case "Login":
                    BindDataGrid("LoginLogstbl");
                    break;
                case "Patient":
                    BindDataGrid("PatientLogstbl");
                    break;
                case "Consultation":
                    BindDataGrid("ConsultationLogstbl");
                    break;
                case "Medicine":
                    BindDataGrid("MedicineLogstbl");
                    break;
                default:
                    break;
            }
        }

        private void BindDataGrid(string logList)
        {
            string query = string.Empty;

            switch(logList)
            {
                case "LoginLogstbl":
                    query = "SELECT * FROM LoginLogstbl";
                    break;
                case "PatientLogstbl":
                    query = "SELECT * FROM PatientLogstbl";
                    break;
                case "ConsultationLogstbl":
                    query = "SELECT * FROM ConsultationLogstbl";
                    break;
                case "MedicineLogstbl":
                    query = "SELECT * FROM MedicineLogstbl";
                    break;
            }
            
            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable(logList);
                        adapter.Fill(dataTable);

                        LogsTable.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }

    public class ComboBoxItems
    {
        public List<string> ComboBoxData { get; set; }

        public ComboBoxItems()
        {
            ComboBoxData = new List<string>()
            {
                "Login",
                "Patient",
                "Consultation",
                "Medicine"
            };
        }
    }
}
