using LogicaDeNegocios.ClasesDominio;
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

namespace InterfazDeUsuario.GUITipoDeSesion
{
    /// <summary>
    /// Lógica de interacción para GUICoordinador.xaml
    /// </summary>
    public partial class GUICoordinador : Window
    {
        public GUICoordinador(Sesion sesion)
        {
            InitializeComponent();
        }

        private void BtnValidarAlumno_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
