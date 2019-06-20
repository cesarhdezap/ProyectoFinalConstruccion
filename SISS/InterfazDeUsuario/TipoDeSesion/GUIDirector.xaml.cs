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
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

namespace InterfazDeUsuario.GUITipoDeSesion
{
    /// <summary>
    /// Interaction logic for GUIDirector.xaml
    /// </summary>
    public partial class GUIDirector : Window
    {
        private Director Director { get; set; }
        public GUIDirector(Sesion sesion)
        {
			Mouse.OverrideCursor = Cursors.Wait;
            InitializeComponent();
            DirectorDAO directorDAO = new DirectorDAO();
            
            try
            {
                Director = directorDAO.CargarDirectorPorIDPersonal(int.Parse(sesion.IDUsuario));
            }
			catch (AccesoADatosException ex)
			{
				MostrarMessageBoxDeExcepcion(this, ex);
				Close();
			}
			finally
			{
				Mouse.OverrideCursor = null;
			}

			LabelNombreDeUsuario.Content = Director.Nombre;
        }

        private void ButtonBuscarCoordinador_Click(object sender, RoutedEventArgs e)
        {
            GUIsDeDirector.GUIBuscarCoordinador buscarCoordinador = new GUIsDeDirector.GUIBuscarCoordinador(Director);
			MostrarPantalla(this, buscarCoordinador);
		}

		private void RegistrarCoordinador_Click(object sender, RoutedEventArgs e)
        {
            GUIsDeDirector.GUIRegistrarCoordinador registrarCoordinador = new GUIsDeDirector.GUIRegistrarCoordinador(Director);
			MostrarPantalla(this, registrarCoordinador);
        }

		private void LabelCerrarSesion_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Close();
		}
	}
}
