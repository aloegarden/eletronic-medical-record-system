using AppDev_Final_Project__Medical_Records_.objects;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AppDev_Final_Project__Medical_Records_.pages.subpage.patient
{
    /// <summary>
    /// Interaction logic for Consultation.xaml
    /// </summary>
    public partial class Consultation : Page
    {
        consultation consultation = new consultation();
        Consult consultationInfo = new Consult();
        logCommands logCommands = new logCommands();
        private UserLogin user;
        public Consultation(UserLogin user)
        {
            InitializeComponent();
            this.user = user;
            DataContext = new BPComboboxItem();
            PopulateTable();
        }
        private void PopulateTable()
        {
            patient pat = new patient();
            List<Patient> patients = pat.GetAllPatients();

            if (patients != null)
            {
                foreach (Patient patient in patients)
                {
                    PatientDataGrid.Items.Add(patient);
                }
            }


        }
        private void DisplaySearchResultTable()
        {
            patient pat = new patient();
            List<Patient> searchResult = pat.SearchPatient(txtSearch.Text);

            if (searchResult != null)
            {
                foreach (Patient result in searchResult)
                {
                    PatientDataGrid.Items.Add(result);
                }
            }

        }
        private void btnPressure_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bpFirst.Text != null || bpSecond.Text != null)
                {
                    consultationInfo.bpFirst = Convert.ToInt32(bpFirst.Text.Trim());
                    consultationInfo.bpSecond = Convert.ToInt32(bpSecond.Text.Trim());
                    txtPressure.Text = consultationInfo.Bp_remarks.ToString();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Please input blood pressure values.");
                return;
            }
        }

        private void btnTempRemarks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtTemp.Text != null)
                {
                    consultationInfo.temperature = Convert.ToDecimal(txtTemp.Text.Trim());
                    decimal temperature = consultationInfo.temperature;

                    if (temperature <= 35M)
                    {
                        txtTempRemarks.Text = "Hypothermia";
                    }
                    else if (temperature >= 35.05M && temperature <= 36.05M)
                    {
                        txtTempRemarks.Text = "Possibly normal";
                    }
                    else if (temperature >= 36.1M && temperature <= 37M)
                    {
                        txtTempRemarks.Text = "Normal";
                    }
                    else if(temperature >= 38M && temperature <= 39.4M)
                    {
                        txtTempRemarks.Text = "Fever";
                    }
                    else
                    {
                        txtTempRemarks.Text = "High Fever";
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Please input temperature value.");
                return;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InputFieldValid())
                {
                    consultationInfo.patient_id = Convert.ToInt32(txtId.Text);
                    consultationInfo.weight = Convert.ToDecimal(txtWeight.Text);
                    consultationInfo.height = Convert.ToDecimal(txtHeight.Text);
                    consultationInfo.bpFirst = Convert.ToInt32(bpFirst.Text);
                    consultationInfo.bpSecond = Convert.ToInt32(bpSecond.Text);
                    consultationInfo.bp = bpFirst.Text + "/" + bpSecond.Text;
                    consultationInfo.Bp_remarks = txtPressure.Text.ToString();
                    consultationInfo.temperature = Convert.ToDecimal(txtTemp.Text); 
                    consultationInfo.temperature_remarks = txtTempRemarks.Text.ToString();
                    consultationInfo.consultation_date = (DateTime)consultationDate.SelectedDate;

                    consultationInfo.id = consultation.InsertConsultation(consultationInfo);
                    logCommands.InsertToConsultationLogs(user, consultationInfo, "Added Consultation");

                    MessageBox.Show("Consultation Added!");
                    ClearFields();
                }
                else
                {
                    
                    MessageBox.Show("Please select a patient and consultation date.");
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                RefreshTable();
                DisplaySearchResultTable();
                txtSearch.Text = null;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshTable();
            PopulateTable();
        }

        private void ClearFields()
        {
            txtId.Text = null;
            txtWeight.Text = null;
            txtHeight.Text = null;
            bpFirst.Text = null;
            bpSecond.Text = null;
            txtPressure.Text = null;
            txtTemp.Text = null;
            txtTempRemarks = null;
            consultationDate.Text = null;
        }

        private void RefreshTable()
        {
            PatientDataGrid.Items.Clear();
            PatientDataGrid.Items.Refresh();
        }

        private bool InputFieldValid()
        {
            if (txtId.Text == null || consultationDate.SelectedDate == null)
            {
                return false;
            }

            return true;
        }

        private void PatientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Patient pt = PatientDataGrid.SelectedItem as Patient;

            if (pt != null)
            {
                txtId.Text = pt.id.ToString();
            }

        }
    }

    public class BPComboboxItem
    {
        public List<Int32> BPData { get; set; }
        public BPComboboxItem()
        {
            BPData = new List<Int32>()
            {
                70,
                80,
                90,
                100,
                110,
                120,
                130,
                140,
                150,
                160,
                170,
                180,
                190,
                200,
                210,
                220,
                230,
                240,
                250
            };
        }
    }
}
