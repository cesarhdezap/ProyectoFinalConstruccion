using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.Excepciones;
using System.Windows;
using System.Windows.Input;
using InterfazDeUsuario.GUIsDeAlumno;

namespace InterfazDeUsuario.GUITipoDeSesion
{
    public partial class GUIAlumno : Window
    {
        private Alumno Alumno { get; set; }
        public GUIAlumno(Sesion sesion)
        {
            InitializeComponent();
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            try
            {
                Alumno = alumnoDAO.CargarAlumnoPorMatricula(sesion.IDUsuario);
            }
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
			{
				MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
			{
				MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
			{
				MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
			{
				MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
			}
			finally
			{
				Mouse.OverrideCursor = null;
			}

			if (Alumno.EstadoAlumno == EstadoAlumno.EsperandoAsignacion)
            {
                LabelEsperandoAsignacion.Visibility = Visibility.Visible;
            }
            LabelNombreDeUsuario.Content = Alumno.Nombre;
            LabelEsperandoAsignacion.Visibility = Visibility.Hidden;
            LabelDadoDeBaja.Visibility = Visibility.Hidden;
            LabelLiberado.Visibility = Visibility.Hidden;
            LabelEsperandoAceptacion.Visibility = Visibility.Hidden;
            ButtonEscogerProyecto.Visibility = Visibility.Hidden;
            ButtonVerExpediente.Visibility = Visibility.Hidden;
            switch (Alumno.EstadoAlumno)
            {
                case EstadoAlumno.EsperandoAceptacion:
                    LabelEsperandoAceptacion.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.EsperandoAsignacion:
                    LabelEsperandoAsignacion.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.Aceptado:
                    ButtonEscogerProyecto.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.Asignado:
                    ButtonVerExpediente.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.Liberado:
                    LabelLiberado.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.DadoDeBaja:
                    LabelDadoDeBaja.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.Rechazado:
                    LabelDadoDeBaja.Visibility = Visibility.Visible;
                break;
            }
        }

        private void ButtonEscogerProyecto_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            GUIEscogerProyectos escogerProyectos = new GUIEscogerProyectos(Alumno);
            escogerProyectos.ShowDialog();
            ShowDialog();
            Mouse.OverrideCursor = Cursors.Wait;
			AlumnoDAO alumnoDAO = new AlumnoDAO();
			try
			{
				Alumno = alumnoDAO.CargarAlumnoPorMatricula(Alumno.Matricula);
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
			if (Alumno.EstadoAlumno == EstadoAlumno.EsperandoAsignacion)
            {
                LabelEsperandoAsignacion.Visibility = Visibility.Visible;
                ButtonEscogerProyecto.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonVerExpediente_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            GUIVerExpedientePorAlumno verExpediente = new GUIVerExpedientePorAlumno(Alumno);
            verExpediente.ShowDialog();
            ShowDialog();
        }
    }
}