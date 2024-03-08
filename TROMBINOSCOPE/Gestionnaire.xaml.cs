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

namespace AppTrombinoscope
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Gestionnaire : Window
    {
        public Gestionnaire()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(usernameInput.Text == "admin" && pwdInput.Password.ToString() == "Password1234!")
            {
                AppCache.isGestionnaire = true;
                Close();
            }
        }
    }
}
