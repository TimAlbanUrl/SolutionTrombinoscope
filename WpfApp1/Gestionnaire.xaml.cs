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
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Gestionnaire: Window
    {
        public Gestionnaire()
        {
            InitializeComponent();
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            if(Login.Text == "admin" && Password.Password == "Password1234!")
            {
                AppTrombinoscope.AppCache.isLoggedIn = true;
                Window mainWindow = Application.Current.MainWindow;
                mainWindow.Title = mainWindow.Title + " (connecté)";
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
