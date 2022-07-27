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

namespace AppDev_Final_Project__Medical_Records_.pages.subpage.patient
{
    /// <summary>
    /// Interaction logic for ViewPatients.xaml
    /// </summary>
    public partial class ViewPatients : Page
    {
        patient pat = new patient();
        public ViewPatients()
        {
            InitializeComponent();
            PopulateTable();
        }

        private void PopulateTable()
        {
            
            List<Patient> patients = pat.GetAllPatients();
            if(patients != null)
            {
                foreach (Patient patient in patients)
                {
                    ViewPatientDataGrid.Items.Add(patient);
                }
            }
        }

        private void RefreshTable()
        {
            ViewPatientDataGrid.Items.Clear();
            ViewPatientDataGrid.Items.Refresh();
        }

        private void DisplaySearchResults()
        {
            List<Patient> searchResult = pat.SearchPatient(txtSearch.Text);

            if (searchResult != null)
            {
                foreach (Patient result in searchResult)
                {
                    ViewPatientDataGrid.Items.Add(result);
                }
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(txtSearch.Text != "")
            {
                RefreshTable();
                DisplaySearchResults();
                txtSearch.Text = null;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshTable();
            PopulateTable();
        }

        
    }
}
