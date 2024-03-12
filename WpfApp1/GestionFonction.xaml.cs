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
    /// Logique d'interaction pour GestionFonction.xaml
    /// </summary>
    public partial class GestionFonction : Window
    {
        private BddPersonnels bdd;

        public GestionFonction()
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

        private void ModifiyButton_Click(object sender, RoutedEventArgs e)
        {
            bdd.UpdateFonction(FonctionList.SelectedItem, ModifyInput.Text);
            ModifyInput.Text = "";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            bdd.DeleteFonction(FonctionList.SelectedItem);
            UpdateList();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            bdd.InsertFonction(CreateInput.Text);
            UpdateList();
            CreateInput.Text = "";
        }

        private void UpdateList()
        {
            List<BddpersonnelContext.Fonction> listeFonctions = bdd.FetchAllFonctions();
            FonctionList.ItemsSource = listeFonctions;
        }
    }
}
