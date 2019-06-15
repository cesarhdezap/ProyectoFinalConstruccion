using LogicaDeNegocios.ClasesDominio;
using System.Windows;
using static LogicaDeNegocios.Servicios.ServiciosDeSesion;
using static LogicaDeNegocios.Servicios.ServiciosDeAutenticacion;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            TextBoxCorreo.MaxLength = TAMAÑO_MAXIMO_VARCHAR;
            PasswordBoxContraseña.MaxLength = TAMAÑO_MAXIMO_VARCHAR;
            
        }



        private void ButtonIngresar_Click(object sender, RoutedEventArgs e)
        {
            string correo = TextBoxCorreo.Text;
            if (ValidarCadena(correo) && ValidarCadena(PasswordBoxContraseña.Password))
            {
				Mouse.OverrideCursor = Cursors.Wait;
                bool resultadoDeAutenticacion = false;
                try
                {
                    resultadoDeAutenticacion = AutenticarCredenciales(correo, PasswordBoxContraseña.Password);
                }
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
				{
					MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
				{
					MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
				{
					MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
				{
					MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
				{
					MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("Usuario o contraseña no validos.", "Credenciales no validas", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar su contraseña y su correo para continuar.", "Campos vacios", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Tipo de sesion no valida. Contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
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
