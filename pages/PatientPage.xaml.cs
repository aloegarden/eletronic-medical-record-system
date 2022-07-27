using AppDev_Final_Project__Medical_Records_.objects;
using AppDev_Final_Project__Medical_Records_.pages.subpage.patient;
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

namespace AppDev_Final_Project__Medical_Records_.pages
{
    /// <summary>
    /// Interaction logic for PatientPage.xaml
    /// </summary>
    public partial class PatientPage : Page
    {
        private UserLogin user;

        public PatientPage(UserLogin user)
        {
            InitializeComponent();
            this.user = user;
            Main.Content = new AddPatient(user);
        }
        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new AddPatient(user);
        }

        private void btnUpDelPatient_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new UpdatePatient(user);
        }

        private void btnViewPatient_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ViewPatients();
        }

        private void btnConsultation_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Consultation(user);
        }

        private void btnViewConsultations_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ViewConsultation();
        }
    }
}
