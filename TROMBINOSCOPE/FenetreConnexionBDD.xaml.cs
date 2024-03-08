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

namespace AppTrombinoscope
{
    /// <summary>
    /// Logique d'interaction pour FenetreConnexionBDD.xaml
    /// </summary>
    public partial class FenetreConnexionBDD : Window
    {
        public FenetreConnexionBDD()
        {
            InitializeComponent();
        }
        public string username
        {
            get { return ChampUtilisateur.Text; }
            set { ChampUtilisateur.Text = value; }
        }
        public string adress
        {
            get { return ChampIP.Text; }
            set { ChampIP.Text = value; }
        }
        public string port
        {
            get { return ChampPort.Text; }
            set { ChampPort.Text = value; }
        }
        public string password
        {
            get { return ChampMotDePasse.Password; }
            set { ChampMotDePasse.Password = value; }
        }


        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            AppCache.bdd = new DllbddPersonnels.BddPersonnels(username, password, adress, port);
        }
    }
}
