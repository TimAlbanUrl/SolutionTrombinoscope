using DllbddPersonnels;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour GestionService.xaml
    /// </summary>
    public partial class GestionService : Window
    {
        private BddPersonnels bdd;

        public GestionService()
        {
            InitializeComponent();
            try
            {
                bdd = new BddPersonnels(Properties.Settings.Default.Username,
                Properties.Settings.Default.Password, Properties.Settings.Default.IP,
                Properties.Settings.Default.Port);
                UpdateList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur lors de l'ouverture des parametres !");
            }
        }

        private void UpdateList()
        {
            List<BddpersonnelContext.Service> listeServices = bdd.FetchAllServices();
            ServiceList.ItemsSource = listeServices;
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            bdd.UpdateService(ServiceList.SelectedItem, ModifyInput.Text);
            ModifyInput.Text = "";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            bdd.DeleteService(ServiceList.SelectedItem);
            UpdateList();
        }

        private void CreteButton_Click(object sender, RoutedEventArgs e)
        {
            bdd.InsertService(CreateInput.Text);
            UpdateList();
            CreateInput.Text = "";
        }
    }
}
