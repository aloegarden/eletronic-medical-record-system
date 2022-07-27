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
    /// Interaction logic for ViewConsultation.xaml
    /// </summary>
    public partial class ViewConsultation : Page
    {
        consultation consultation = new consultation();
        public ViewConsultation()
        {
            InitializeComponent();
            PopulateTable();
        }

        private void PopulateTable()
        {
            List<Consult> consultationInfo = consultation.GetAllConsultations();
            if(consultationInfo != null)
            {
                foreach(Consult consultation in consultationInfo)
                {
                    ViewConsultationDataGrid.Items.Add(consultation);
                }
            }
        }

        private void DisplaySearchResults()
        {
            List<Consult> searchResults = consultation.SearchConsultations(txtSearch.Text);

            if(searchResults != null)
            {
                foreach(Consult searchResult in searchResults)
                {
                    ViewConsultationDataGrid.Items.Add(searchResult);
                }
            }
        }

        private void RefreshTable()
        {
            ViewConsultationDataGrid.Items.Clear();
            ViewConsultationDataGrid.Items.Refresh();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(txtSearch.Text != "")
            {
                RefreshTable();
                DisplaySearchResults();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshTable();
            PopulateTable();
        }
    }
}
