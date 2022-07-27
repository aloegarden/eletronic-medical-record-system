using AppDev_Final_Project__Medical_Records_.objects;
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

namespace AppDev_Final_Project__Medical_Records_.pages.subpage.medicine
{
    /// <summary>
    /// Interaction logic for ReleaseMedicine.xaml
    /// </summary>
    public partial class ReleaseMedicine : Page
    {
        private UserLogin user;
        medicineSQLCommands SQLCommands = new medicineSQLCommands();
        logCommands logCommands = new logCommands();
        Patient patient = new Patient();
        Medicine medicine = new Medicine();
        Release_Medicine releaseMed = new Release_Medicine();
        public ReleaseMedicine(UserLogin user)
        {
            this.user = user;
            InitializeComponent();
            PopulateTable();
        }
        private void PopulateTable()
        {
            List<Medicine> medicines = SQLCommands.GetMedicines();

            if (medicines != null)
            {
                foreach (Medicine medicine in medicines)
                {
                    datagrid_releaseMedicine.Items.Add(medicine);
                }
            }
        }

        private void btn_releaseMed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InputValid())
                {
                   
                    releaseMed.patient_id = Convert.ToInt32(txt_PatientId.Text);
                    releaseMed.Id = Convert.ToInt32(txt_MedicineId.Text);
                    int quan = releaseMed.quantity = Convert.ToInt32(txt_Quantity.Text);
                    releaseMed.reason = txt_Reason.Text.ToString();
                    
                    string checkQuantity=SQLCommands.CheckMedQuantity(releaseMed);
                    int cQuantity = Convert.ToInt32(checkQuantity);

                    bool ifValid = SQLCommands.checksId(releaseMed);
                    
                    if (ifValid) 
                    {
                        
                        if (cQuantity >= quan)
                        {
                            releaseMed.quantity = quan;
                            string quantity = SQLCommands.UpdateReleaseMedicine(releaseMed); 
                            string result = SQLCommands.ReleaseMedicine(releaseMed);
                            logCommands.InsertReleasedMedicineToLogs(releaseMed, user, "Released Medicine");

                            MessageBox.Show(result + "\n" + quantity);
                            ResetFields();

                        }
                        else
                        {
                            MessageBox.Show("Out of Stock Medicine");
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("PatientId does not exist");
                    }
                    
                    
                }
                else
                {
                    MessageBox.Show("Please do not leave fields blank.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please double check the inputted fields.");
            }

        }

        private void datagrid_releaseMedicine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medicine med = datagrid_releaseMedicine.SelectedItem as Medicine;
            if (med != null)
            {

                txt_MedicineId.Text = med.Id.ToString();
                releaseMed.medicine_name = med.medicine_name.ToString();
            }
        }
        private bool InputValid()
        {
            if (txt_PatientId == null || txt_MedicineId == null || txt_Quantity == null || txt_Reason == null)
            {
                return false;
            }

            return true;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshTable();
            PopulateTable();
        }

        private void RefreshTable()
        {
            datagrid_releaseMedicine.Items.Clear();
            datagrid_releaseMedicine.Items.Refresh();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                RefreshTable();
                DisplaySearchResult();
            }
        }

        private void ResetFields()
        {
            txt_PatientId.Clear();
            txt_MedicineId.Clear();
            txt_Quantity.Clear();
            txt_Reason.Clear();

        }

        private void DisplaySearchResult()
        {
            List<Medicine> medicines = SQLCommands.GetMedicine(txtSearch.Text);

            if (medicines != null)
            {
                foreach (Medicine medicine in medicines)
                {
                    datagrid_releaseMedicine.Items.Add(medicine);
                }
            }
        }

        private static readonly Regex _regex = new Regex("[^0-9]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void txtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


    }
}
