using AppDev_Final_Project__Medical_Records_.objects;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppDev_Final_Project__Medical_Records_.pages.subpage.medicine
{
    /// <summary>
    /// Interaction logic for ManageMedicine.xaml
    /// </summary>
    public partial class ManageMedicine : Page
    {
        private UserLogin user;
        logCommands logCommands = new logCommands();
        medicineSQLCommands SQLCommands = new medicineSQLCommands();
        public ManageMedicine(UserLogin user)
        {
            this.user = user;
            InitializeComponent();
            PopulateTable();
        }

        private void PopulateTable()
        {
            List<Medicine> medicines = SQLCommands.GetMedicines();

            if(medicines != null)
            {
                foreach(Medicine medicine in medicines)
                {
                    DataGridMedicine.Items.Add(medicine);
                }
            }
        }

        private void DisplaySearchResult()
        {
            List<Medicine> medicines = SQLCommands.GetMedicine(txtSearch.Text);

            if(medicines != null)
            {
                foreach(Medicine medicine in medicines)
                {
                    DataGridMedicine.Items.Add(medicine);
                }
            }
        }
        
        private void RefreshTable()
        {
            DataGridMedicine.Items.Clear();
            DataGridMedicine.Items.Refresh();
        }

        private bool InputValid()
        {
            if(txtName == null || txtDescription == null || txtQuantity == null)
            {
                return false;
            }

            return true;
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(InputValid())
                {
                    Medicine medicine = new Medicine();
                    medicine.medicine_name = txtName.Text.ToString();
                    medicine.description = txtDescription.Text.ToString();
                    medicine.quantity = Convert.ToInt32(txtQuantity.Text);
                    string result = SQLCommands.InsertMedicine(medicine);
                    
                    if(result != null)
                    {
                        logCommands.InsertMedicineToLogs(medicine, user, "Added Medicine");
                        MessageBox.Show("Added Medicine!");
                    }
                    else
                    {
                        MessageBox.Show("Not Added");
                    }

                }
                else
                {
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please do not leave fields blank.");
            }

            ResetFields();
        }

        private void EnableFields()
        {
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnAdd.IsEnabled = true;
        }

        private void ResetFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtQuantity.Clear();

            btnAdd.IsEnabled = true;
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InputValid())
                {
                    Medicine medicine = new Medicine();
                    medicine.Id = Convert.ToInt32(txtID.Text);
                    medicine.medicine_name = txtName.Text.ToString();
                    medicine.description = txtDescription.Text.ToString();
                    medicine.quantity = Convert.ToInt32(txtQuantity.Text);

                    string result = SQLCommands.UpdateMedicine(medicine);

                    if (result != null)
                    {
                        logCommands.InsertMedicineToLogs(medicine, user, "Updated Medicine");
                        MessageBox.Show("Updated Medicine.");
                    }
                }
                else
                {
                    MessageBox.Show("Please do not leave fields blank.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please do not leave fields blank.");
            }

            ResetFields();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Medicine medicine = new Medicine();
                medicine.Id = Convert.ToInt32(txtID.Text);
                medicine.medicine_name = txtName.Text.ToString();
                medicine.description = txtDescription.Text.ToString();
                medicine.quantity = Convert.ToInt32(txtQuantity.Text);

                string result = SQLCommands.DeleteMedicine(medicine);
                

                if(result != null)
                {
                    logCommands.InsertMedicineToLogs(medicine, user, "Deleted Medicine");
                    MessageBox.Show("Deleted Medicine.");
                }
                else
                {
                    MessageBox.Show("Null");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ResetFields();
        }

        private void txtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(txtSearch.Text != "")
            {
                RefreshTable();
                DisplaySearchResult();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshTable();
            PopulateTable();
        }

        private void DataGridMedicine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medicine med = DataGridMedicine.SelectedItem as Medicine;
            if(med != null)
            {
                EnableFields();
                txtID.Text = med.Id.ToString();
                txtName.Text = med.medicine_name.ToString();
                txtDescription.Text = med.description.ToString();
                txtQuantity.Text = med.quantity.ToString();
            }
        }
    }
}
