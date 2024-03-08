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
using AppTrombinoscope;

namespace TROMBINOSCOPE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FenetreConnexionBDD paramBDD = new FenetreConnexionBDD ();
                paramBDD.ChampIP.Text = AppTrombinoscope.Properties.Settings.Default.IP;
                paramBDD.ChampPort.Text = AppTrombinoscope.Properties.Settings.Default.Port;
                paramBDD.ChampUtilisateur.Text = AppTrombinoscope.Properties.Settings.Default.Username;
                paramBDD.ChampMotDePasse.Password = AppTrombinoscope.Properties.Settings.Default.Password;
                if (paramBDD.ShowDialog() == true)
                {
                    AppTrombinoscope.Properties.Settings.Default.IP = paramBDD.ChampIP.Text;
                    AppTrombinoscope.Properties.Settings.Default.Port = paramBDD.ChampPort.Text;
                    AppTrombinoscope.Properties.Settings.Default.Username = paramBDD.ChampUtilisateur.Text;
                    AppTrombinoscope.Properties.Settings.Default.Password = paramBDD.ChampMotDePasse.Password;
                    AppTrombinoscope.Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur lors de l'ouverture des parametres !");
            }
        }
    }
}