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

namespace AppDev_Final_Project__Medical_Records_.pages.subpage.medicine
{
    /// <summary>
    /// Interaction logic for ViewReleasedMedicine.xaml
    /// </summary>
    public partial class ViewReleasedMedicine : Page
    {
        //private UserLogin user;
        medicineSQLCommands SQLCommands = new medicineSQLCommands();

        public ViewReleasedMedicine()
        {
            InitializeComponent();
            PopulateTable();
        }

        private void PopulateTable()
        {
            List<Release_Medicine> ReleaseMedicines = SQLCommands.GetReleaseMedicines();

            if (ReleaseMedicines != null)
            {
                foreach (Release_Medicine releaseMed in ReleaseMedicines)
                {
                    datagrid_releaseMed.Items.Add(releaseMed);
     
                }
            }
            else
            {
                MessageBox error;
            }

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
