using InterfazDeUsuario.GUIsDeAlumno;
using InterfazDeUsuario.GUITipoDeSesion;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using System.Windows;
using System.Windows.Input;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static LogicaDeNegocios.Servicios.ServiciosDeAutenticacion;
using static LogicaDeNegocios.Servicios.ServiciosDeSesion;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace InterfazDeUsuario
{
	/// <summary>
	/// Login general para cada usuario.
	/// Contiene metodos de autenticación para el correo y contraseña.
	/// </summary>

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
                bool excepcionTirada = false;
                try
                {
                    resultadoDeAutenticacion = AutenticarCredenciales(correo, PasswordBoxContraseña.Password);
                }
				catch (AccesoADatosException ex)
				{
                    resultadoDeAutenticacion = false;
                    excepcionTirada = true;
					MostrarMessageBoxDeExcepcion(this, ex);
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}

                if (!excepcionTirada)
                {
                    if (resultadoDeAutenticacion)
                    {
                        Sesion sesion = new Sesion();

                        try
                        {
                            sesion = CargarSesion(correo);
                        }
                        catch (AccesoADatosException ex)
                        {
                            MostrarMessageBoxDeExcepcion(this, ex);
                        }

                        Hide();
                        InstanciarVentanaDeSesion(sesion);
                        Show();
                        TextBoxCorreo.Clear();
                        PasswordBoxContraseña.Clear();
                    }
                    else
                    {
                        MessageBox.Show(this, CREDENCIALES_INVALIDAS_MENSAJE, CREDENCIALES_INVALIDAS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(this, CAMPOS_LOGIN_VACIOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Declara e instancia la interfaz de usuario segun el tipo de sesión.
        /// </summary>
        /// <param name="sesion"> Un objeto tipo Sesion</param>
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
                MessageBox.Show(this, TIPO_DE_SESION_INVALIDO_MENSAJE, ERROR_INTERNO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonRegistrarseComoAlumno_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistrarAlumno registrarAlumno = new GUIRegistrarAlumno();
			MostrarPantalla(this, registrarAlumno);
			TextBoxCorreo.Clear();
			PasswordBoxContraseña.Clear();
		}

		private void Window_Closed(object sender, System.EventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
