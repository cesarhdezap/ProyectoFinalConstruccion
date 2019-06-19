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

            LabelNombreDeUsuario.Content = Alumno.Nombre;
            OcultarElementosGraficos();
            MostrarElementosGraficosPorEstadoAlumno();
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
            OcultarElementosGraficos();
            MostrarElementosGraficosPorEstadoAlumno();
             
        }

        private void OcultarElementosGraficos()
        {
            LabelEsperandoAsignacion.Visibility = Visibility.Hidden;
            LabelDadoDeBaja.Visibility = Visibility.Hidden;
            LabelLiberado.Visibility = Visibility.Hidden;
            LabelEsperandoAceptacion.Visibility = Visibility.Hidden;
            ButtonEscogerProyecto.Visibility = Visibility.Hidden;
            ButtonVerExpediente.Visibility = Visibility.Hidden;
        }

        private void MostrarElementosGraficosPorEstadoAlumno()
        {
            switch (Alumno.EstadoAlumno)
            {
                case EstadoAlumno.EsperandoAceptacion:
                    LabelEsperandoAceptacion.Visibility = Visibility.Visible;
                    break;
                case EstadoAlumno.Aceptado:
                    ButtonEscogerProyecto.Visibility = Visibility.Visible;
                    break;
                case EstadoAlumno.EsperandoAsignacion:
                    LabelEsperandoAsignacion.Visibility = Visibility.Visible;
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

        private void ButtonVerExpediente_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            GUIVerExpedientePorAlumno verExpediente = new GUIVerExpedientePorAlumno(Alumno);
            verExpediente.ShowDialog();
            ShowDialog();
        }

		private void LabelCerrarSesión_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Close();
		}
	}
}