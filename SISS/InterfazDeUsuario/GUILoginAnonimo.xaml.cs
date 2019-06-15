using LogicaDeNegocios.ClasesDominio;
using System.Windows;
using static LogicaDeNegocios.Servicios.ServiciosDeSesion;
using static LogicaDeNegocios.Servicios.ServiciosDeAutenticacion;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using InterfazDeUsuario.GUITipoDeSesion;
using InterfazDeUsuario.GUIsDeAlumno;
using LogicaDeNegocios.Excepciones;
using System.Windows.Input;
using System.Security;

namespace InterfazDeUsuario
{
    public partial class GUILoginAnonimo : Window
    {
        public GUILoginAnonimo()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            TextBoxCorreo.MaxLength = TAMAÑO_MAXIMO_VARCHAR;
            PasswordBoxContraseña.MaxLength = TAMAÑO_MAXIMO_VARCHAR;
            
        }



        private void ButtonIngresar_Click(object sender, RoutedEventArgs e)
        {
            string correo = TextBoxCorreo.Text;
			string contraseña = PasswordBoxContraseña.Password;
			if (!string.IsNullOrEmpty(correo) && !string.IsNullOrEmpty(contraseña))
            {
				Mouse.OverrideCursor = Cursors.Wait;
                bool resultadoDeAutenticacion = false;
                try
                {
                    resultadoDeAutenticacion = AutenticarCredenciales(correo, PasswordBoxContraseña.Password);
                }
				catch (AccesoADatosException ex)
				{
					MensajeDeErrorParaMessageBox mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox();
					mensajeDeErrorParaMessageBox = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(ex);
					MessageBox.Show(this, mensajeDeErrorParaMessageBox.Mensaje, mensajeDeErrorParaMessageBox.Titulo, MessageBoxButton.OK, MessageBoxImage.Error);
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}

                if (resultadoDeAutenticacion)
                {
                    Sesion sesion = CargarSesion(correo);
                    Hide();
                    InstanciarVentanaDeSesion(sesion);
                    Show();
					TextBoxCorreo.Clear();
					PasswordBoxContraseña.Clear();
                }
                else 
                {
                    MessageBox.Show(CREDENCIALES_INVALIDAS_MENSAJE, CREDENCIALES_INVALIDAS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(CAMPOS_LOGIN_VACIOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(TIPO_DE_SESION_INVALIDO_MENSAJE, ERROR_INTERNO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonRegistrarseComoAlumno_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistrarAlumno registrarAlumno = new GUIRegistrarAlumno();
            Hide();
            registrarAlumno.ShowDialog();
            Show();
			TextBoxCorreo.Clear();
			PasswordBoxContraseña.Clear();
		}

		private void Window_Closed(object sender, System.EventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
