using AppDev_Final_Project__Medical_Records_.objects;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppDev_Final_Project__Medical_Records_.pages.subpage.patient
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Page
    {
        patient patient = new patient();
        logCommands logCommands = new logCommands();
        private UserLogin user;

        public AddPatient(UserLogin user)
        {
            InitializeComponent();
            this.user = user;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (AddFieldValid())
            {
                MessageBox.Show("Please do not leave any fields blank");
                return;
            }
            else
            {
                try
                {
                    Patient patientInfo = new Patient();
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

                    int patientId = patient.InsertPatient(patientInfo);

                    if(patientId > 0)
                    {
                        patientInfo.id = patientId;
                        logCommands.InsertToPatientLogs(user, patientInfo, "Added Patient");

                        MessageBox.Show("Patient Added!");
                    }
                    else
                    {
                        MessageBox.Show("An error occured. Please try again.");
                    }
                    

                    ClearFields();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private bool AddFieldValid()
        {
            if (txtFname.Text == null || txtLname.Text == null || txtContact.Text == null || txtAge.Text == null || txtAddress.Text == null ||
                dob.SelectedDate == null || (lblMale.IsChecked == false && lblFemale.IsChecked == false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ClearFields()
        {
            txtFname.Clear();
            txtLname.Clear();
            txtAddress.Clear();
            txtAge.Clear();
            txtContact.Clear();
            dob.SelectedDate = null;
            if (lblMale.IsChecked == true)
            {
                lblMale.IsChecked = false;
            }
            else
            {
                lblFemale.IsChecked = false;
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
    }
}
