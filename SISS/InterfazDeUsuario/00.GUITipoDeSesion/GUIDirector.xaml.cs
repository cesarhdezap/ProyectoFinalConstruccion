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
    /// Interaction logic for GUIDirector.xaml
    /// </summary>
    public partial class GUIDirector : Window
    {
        public GUIDirector(Sesion sesion)
        {
            InitializeComponent();
        }
    }
}
