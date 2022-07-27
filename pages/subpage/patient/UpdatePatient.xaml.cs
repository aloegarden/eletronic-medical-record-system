using AppDev_Final_Project__Medical_Records_.objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppDev_Final_Project__Medical_Records_.pages.subpage.patient
{
    /// <summary>
    /// Interaction logic for UpdatePatient.xaml
    /// </summary>
    public partial class UpdatePatient : Page
    {
        patient patient = new patient();
        private UserLogin user;
        logCommands logCommands = new logCommands();
        public UpdatePatient(UserLogin user)
        {
            InitializeComponent();
            this.user = user;
            PopulateTable();
        }
        private void PopulateTable()
        {
            patient pat = new patient();
            List<Patient> patients = pat.GetAllPatients();

            if(patients != null)
            {
                foreach (Patient patient in patients)
                {
                    UpdatePatientDataGrid.Items.Add(patient);
                }
            }
            

        }

        private void DisplaySearchResultTable()
        {
            patient pat = new patient();
            List<Patient> searchResult = pat.SearchPatient(txtSearch.Text);

            if(searchResult != null)
            {
                foreach (Patient result in searchResult)
                {
                    UpdatePatientDataGrid.Items.Add(result);
                }
            }
            
        }

        private void txtAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void UpdatePatientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Patient pt = UpdatePatientDataGrid.SelectedItem as Patient;

            EnableFields();
            if(pt != null)
            {
                txtFname.Text = pt.fname.ToString();
                txtLname.Text = pt.lname.ToString();

                if (pt.gender.Equals("Male"))
                {
                    lblMale.IsChecked = true;
                }
                else
                {
                    lblFemale.IsChecked = true;
                }

                txtId.Text = pt.id.ToString();
                txtAge.Text = pt.age.ToString();
                dob.Text = pt.dob.ToString();
                txtContact.Text = pt.contact.ToString();
                txtAddress.Text = pt.address.ToString();
            }
            
        }

        private void EnableFields()


        {
            txtFname.IsEnabled = true;
            txtLname.IsEnabled = true;
            lblFemale.IsEnabled = true;
            lblMale.IsEnabled = true;
            txtAge.IsEnabled = true;
            dob.IsEnabled = true;
            txtContact.IsEnabled = true;
            txtAddress.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;
        }

        private void DisableFields()
        {
            txtFname.IsEnabled = false;
            txtLname.IsEnabled = false;
            lblFemale.IsEnabled = false;
            lblMale.IsEnabled = false;
            txtAge.IsEnabled = false;
            dob.IsEnabled = false;
            txtContact.IsEnabled = false;
            txtAddress.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
        }

        private void ClearFields()
        {
            txtId.Clear();
            txtFname.Clear();
            txtLname.Clear();
            txtAddress.Clear();
            lblFemale.IsChecked = false;
            lblMale.IsChecked= false;
            txtAge.Clear();
            txtContact.Clear();
            dob.SelectedDate = null;
        }

        private void RefreshTable()
        {
            UpdatePatientDataGrid.Items.Clear();
            UpdatePatientDataGrid.Items.Refresh();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(!PatientSelected())
            {
                MessageBox.Show("Please select a patient from the list.");
                return;
            }
            else
            {
                if(!InputFieldValid())
                {
                    MessageBox.Show("Please fill in all entries.");
                }
                else
                {
                    try
                    {
                        Patient patientInfo = new Patient();
                        patientInfo.id = Convert.ToInt32(txtId.Text);
                        patientInfo.fname = txtFname.Text.Trim().ToString();
                        patientInfo.lname = txtLname.Text.Trim().ToString();
                        patientInfo.address = txtAddress.Text.Trim().ToString();
                        patientInfo.contact = txtContact.Text.Trim().ToString();
                        patientInfo.age = Convert.ToInt32(txtAge.Text);
                        patientInfo.dob = dob.SelectedDate.Value;

                        if (lblMale.IsChecked == true)
                        {
                            patientInfo.gender = "Male";
                        }
                        else
                        {
                            patientInfo.gender = "Female";
                        }

                        string result = patient.UpdatePatient(patientInfo);
                        logCommands.InsertToPatientLogs(user, patientInfo, "Updated Patient");


                        MessageBox.Show(result);

                        DisableFields();
                        ClearFields();
                        RefreshTable();
                        PopulateTable();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
            }
        }

        private bool PatientSelected()
        {
            if(txtId.Text == null)
            {
                return false;
            }

            return true;
        }

        private bool InputFieldValid()
        {
            if(txtFname.Text == null || txtLname.Text == null || txtAge.Text == null || txtContact.Text == null ||
                txtAddress.Text == null || dob.SelectedDate == null || (lblMale.IsChecked == false && lblFemale.IsChecked == false))
            {
                return false;
            }

            return true;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!InputFieldValid())
            {
                MessageBox.Show("Please select a patient from the list.");
                return;
            }
            else
            {
                patient patient = new patient();
                Patient patientInfo = new Patient();
                patientInfo.id = Convert.ToInt32(txtId.Text);
                patientInfo.fname = txtFname.Text.Trim().ToString();
                patientInfo.lname = txtLname.Text.Trim().ToString();
                patientInfo.address = txtAddress.Text.Trim().ToString();
                patientInfo.contact = txtContact.Text.Trim().ToString();
                patientInfo.age = Convert.ToInt32(txtAge.Text);
                patientInfo.dob = dob.SelectedDate.Value;

                if (lblMale.IsChecked == true)
                {
                    patientInfo.gender = "Male";
                }
                else
                {
                    patientInfo.gender = "Female";
                }

                string result = patient.DeletePatient(patientInfo);
                logCommands.InsertToPatientLogs(user, patientInfo, "Deleted Patient");

                MessageBox.Show(result);

                DisableFields();
                ClearFields();
                RefreshTable();
                PopulateTable();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshTable();
            PopulateTable();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(txtSearch.Text != "")
            {
                RefreshTable();
                DisplaySearchResultTable();
                txtSearch.Clear();
            }  
        }
    }
}
