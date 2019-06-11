using LogicaDeNegocios.ClasesDominio;
using System.Windows;
using static LogicaDeNegocios.Servicios.ServiciosDeSesion;
using static LogicaDeNegocios.Servicios.ServiciosDeAutenticacion;
using InterfazDeUsuario.GUITipoDeSesion;
using InterfazDeUsuario.GUIsDeAlumno;
using LogicaDeNegocios.Excepciones;
using System.Windows.Input;

namespace InterfazDeUsuario
{
    public partial class GUILoginAnonimo : Window
    {
        public GUILoginAnonimo()
        {
            InitializeComponent();
        }

        private void ButtonIngresar_Click(object sender, RoutedEventArgs e)
        {
            string correo = TextBoxCorreo.Text;
            if (correo != null && PasswordBoxContraseña.Password != null)
            {
                bool resultadoDeAutenticacion = false;
                try
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    resultadoDeAutenticacion = AutenticarCredenciales(correo, PasswordBoxContraseña.Password);
                }
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
                {
                    MessageBox.Show("No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
                {
                    MessageBox.Show("No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Mouse.OverrideCursor = null;

                if (resultadoDeAutenticacion)
                {
                    Sesion sesion = CargarSesion(correo);
                    Hide();
                    InstanciarVentanaDeSesion(sesion);
                    Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña no validos.");
                }
            }
            else
            {
                MessageBox.Show("No se han detectado datos.");
            }
        }

        private void InstanciarVentanaDeSesion(Sesion sesion)
        {
            if (sesion.TipoDeUsuario == TipoDeSesion.Alumno)
            {
                GUIAlumno interfazAlumno = new GUIAlumno(sesion);
                interfazAlumno.ShowDialog();
            }
            else if (sesion.TipoDeUsuario == TipoDeSesion.Coordinador)
            {
                GUICoordinador interfazCoordinador = new GUICoordinador(sesion);
                interfazCoordinador.ShowDialog();
            }
            else if (sesion.TipoDeUsuario == TipoDeSesion.Director)
            {
                GUIDirector interfazDirector = new GUIDirector(sesion);
                interfazDirector.ShowDialog();
            }
            else if (sesion.TipoDeUsuario == TipoDeSesion.Tecnico)
            {
                GUITecnicoAcademico interfazTecnico = new GUITecnicoAcademico(sesion);
                interfazTecnico.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tipo de sesion no valida.");
            }
        }

        private void ButtonRegistrarseComoAlumno_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistrarAlumno registrarAlumno = new GUIRegistrarAlumno();
            Hide();
            registrarAlumno.ShowDialog();
            Show();
        }

		private void Window_Closed(object sender, System.EventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
