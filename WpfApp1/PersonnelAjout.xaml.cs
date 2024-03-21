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
using BddpersonnelContext;
using Devart.Data.Linq.Mapping;
using DllbddPersonnels;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour PersonnelAjout.xaml
    /// </summary>
    public partial class PersonnelAjout : Window
    {
        private BddPersonnels bdd;

        public PersonnelAjout()
        {
            InitializeComponent();
            bdd = new BddPersonnels();
            imageProfil.Source = new BitmapImage(new Uri("default-avatar-icon.jpg", UriKind.Relative));
            ServiceMenu.DisplayMemberPath = "Intitule";
            ServiceMenu.ItemsSource = bdd.FetchAllServices();
            FunctionMenu.DisplayMemberPath = "Intitule";
            FunctionMenu.ItemsSource = bdd.FetchAllFonctions();

        }

        private void SendImage_Click(object sender, RoutedEventArgs e)
        {
            if (ImageUrl.Text.Length > 0)
            {
                imageProfil.Source = new BitmapImage(new Uri(ImageUrl.Text));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e){
            if (ServiceMenu.SelectionBoxItem != null && FunctionMenu.SelectionBoxItem != null 
                 && ChampNom.Text != "" && ChampPrenom.Text != "" && ChampNumero.Text != "")
            {
                bdd.InsertPersonnel(ChampPrenom.Text,ChampNom.Text,(Service)ServiceMenu.SelectionBoxItem
                ,(Fonction)FunctionMenu.SelectionBoxItem,ChampNumero.Text,(BitmapImage)imageProfil.Source);
            }
        }
    }
}