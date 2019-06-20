using LogicaDeNegocios.ClasesDominio;
using System.Windows;
using System.Windows.Input;
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
