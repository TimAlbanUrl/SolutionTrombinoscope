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
using DllbddPersonnels;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BddPersonnels bdd;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                bdd = new BddPersonnels(Properties.Settings.Default.Username,
                Properties.Settings.Default.Password,Properties.Settings.Default.IP,
                Properties.Settings.Default.Port );
                List<BddpersonnelContext.Service> listeServices = bdd.FetchAllServices();
                List<BddpersonnelContext.Fonction> listeFonctions = bdd.FetchAllFonctions();
                ServiceList.ItemsSource = listeServices;
                FonctionList.ItemsSource = listeFonctions;
                updatePersonnelsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur lors de l'ouverture des parametres !");
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e) //parametre bdd
        {
            try
            {
                FenetreConnexionBDD paramBDD = new FenetreConnexionBDD();
                paramBDD.ChampIP.Text = Properties.Settings.Default.IP;
                paramBDD.ChampPort.Text = Properties.Settings.Default.Port;
                paramBDD.ChampUtilisateur.Text = Properties.Settings.Default.Username;
                paramBDD.ChampMotDePasse.Password = Properties.Settings.Default.Password;
                if (paramBDD.ShowDialog() == true)
                {
                    Properties.Settings.Default.IP = paramBDD.ChampIP.Text;
                    Properties.Settings.Default.Port = paramBDD.ChampPort.Text;
                    Properties.Settings.Default.Username = paramBDD.ChampUtilisateur.Text;
                    Properties.Settings.Default.Password = paramBDD.ChampMotDePasse.Password;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur lors de l'ouverture des parametres !");
            }
        }

        private void StaffList_Click(object sender, RoutedEventArgs e)
        {
            Personnels pers = new Personnels();
            pers.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e) //connexion bdd
        {
            try
            {
                bdd = new BddPersonnels(Properties.Settings.Default.Username,
                Properties.Settings.Default.Password, Properties.Settings.Default.IP,
                Properties.Settings.Default.Port);
                List<BddpersonnelContext.Service> listeServices = bdd.FetchAllServices();
                this.ServiceList.ItemsSource = listeServices;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur lors de la connexion");
            }
        }

        private void Gestionnaire_Click(object sender, RoutedEventArgs e)
        {
            Gestionnaire gest = new Gestionnaire();
            gest.Show();
        }

        private void ManageFunctions_Click(object sender, RoutedEventArgs e)
        {
            GestionFonction gestFonc = new GestionFonction();
            gestFonc.Show();
        }

        private void ManageServices_Click(object sender, RoutedEventArgs e)
        {
            GestionService gestServ = new GestionService();
            gestServ.Show();
        }

        private void ManagePersonels_Click(object sender, RoutedEventArgs e)
        {
            new PersonnelAjout().Show();
        }

        private void DeleteSelectionServ_Click(object sender, RoutedEventArgs e)
        {
            ServiceList.SelectedItem = null;
        }

        private void DeleteSelectionFonc_Click(object sender, RoutedEventArgs e)
        {
            FonctionList.SelectedItem = null;
        }

        private void InputNom_TextChanged(object sender, TextChangedEventArgs e)
        {
            updatePersonnelsList();
        }

        private void updatePersonnelsList()
        {
            PersonnelList.ItemsSource = bdd.SelectPersonnels(ServiceList.SelectedItem, FonctionList.SelectedItem, InputNom.Text);
        }

        private void ServiceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updatePersonnelsList();
        }

        private void FonctionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updatePersonnelsList();
        }

        
    }
}
