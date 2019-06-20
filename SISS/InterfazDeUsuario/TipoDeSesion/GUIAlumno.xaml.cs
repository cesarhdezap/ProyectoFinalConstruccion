using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.Excepciones;
using System.Windows;
using System.Windows.Input;
using InterfazDeUsuario.GUIsDeAlumno;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

namespace InterfazDeUsuario.GUITipoDeSesion
{
    public partial class GUIAlumno : Window
    {
        private Alumno Alumno { get; set; }
        public GUIAlumno(Sesion sesion)
        {
			Mouse.OverrideCursor = Cursors.Wait;
			InitializeComponent();
            AlumnoDAO alumnoDAO = new AlumnoDAO();

            try
            {
                Alumno = alumnoDAO.CargarAlumnoPorMatricula(sesion.IDUsuario);
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

            LabelNombreDeUsuario.Content = Alumno.Nombre;
            OcultarElementosGraficos();
            MostrarElementosGraficosPorEstadoAlumno();
        }

        private void ButtonEscogerProyecto_Click(object sender, RoutedEventArgs e)
        {
            GUIEscogerProyectos escogerProyectos = new GUIEscogerProyectos(Alumno);
			Hide();
            escogerProyectos.ShowDialog();
			OcultarElementosGraficos();
            MostrarElementosGraficosPorEstadoAlumno();
			ShowDialog();
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