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
using DllbddPersonnels;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour Personnel.xaml
    /// </summary>
    public partial class GestionPersonnels : Window
    {
        BddPersonnels bdd;
        BddpersonnelContext.Personnel selected;
        public GestionPersonnels()
        {
            InitializeComponent();
            try
            {
                bdd = new BddPersonnels(Properties.Settings.Default.Username,
                Properties.Settings.Default.Password, Properties.Settings.Default.IP,
                Properties.Settings.Default.Port);
                List<BddpersonnelContext.Personnel>listePersonnel = bdd.FetchAllPersonnels();
                this.DataGridPersonnel.ItemsSource = listePersonnel;
                services.ItemsSource = bdd.FetchAllServices();
                services.DisplayMemberPath = "Intitule";
                fonctions.ItemsSource = bdd.FetchAllFonctions();
                fonctions.DisplayMemberPath = "Intitule";
                selected = listePersonnel.ToArray()[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur lors de l'ouverture des parametres !");
            }
            
        }
        private void MenuContextSupprimerPersonnel_Click(object sender, RoutedEventArgs e)
        {
            bdd.DeletePersonnel(selected);
        }

        private void DataGridPersonnel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = (BddpersonnelContext.Personnel)DataGridPersonnel.SelectedItem;
            this.NomTextBox.Text = selected.Nom;
            this.PrenomTextBox.Text = selected.Prenom;
            this.fonctions.SelectedItem = selected.Fonction;
            this.services.SelectedItem = selected.Service;
        }

        private void NomTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                
            }
            catch (NullReferenceException)
            {

            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bdd.UpdatePersonnel(selected, NomTextBox.Text, PrenomTextBox.Text);
            this.DataGridPersonnel.ItemsSource = bdd.FetchAllPersonnels();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new PersonnelAjout().Show();
        }

        private void services_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = (ComboBox)sender;

            selected.Service = (BddpersonnelContext.Service)box.SelectedItem;

            bdd.bdd.SubmitChanges();
            refreshData();
        }

        private void fonctions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = (ComboBox)sender;

            selected.Fonction = (BddpersonnelContext.Fonction)box.SelectedItem;

            bdd.bdd.SubmitChanges();
            refreshData();
        }

        private void refreshData()
        {
            List<BddpersonnelContext.Personnel> listePersonnel = bdd.FetchAllPersonnels();
            this.DataGridPersonnel.ItemsSource = listePersonnel;
        }
    }
}
