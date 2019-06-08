using System.Windows;
using LogicaDeNegocios.ClasesDominio;

namespace InterfazDeUsuario.GUIsDeDirector
{
    public partial class GUIBuscarCoordinador : Window
    {
        private Director Director { get; set; }
        public GUIBuscarCoordinador(Director director)
        {
            InitializeComponent();
            this.Director = director;
        }
    }
}
