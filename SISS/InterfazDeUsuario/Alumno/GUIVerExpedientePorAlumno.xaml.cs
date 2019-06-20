using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using System.Windows;
using System.Windows.Input;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

namespace InterfazDeUsuario.GUIsDeAlumno
{
	/// <summary>
	/// Interaction logic for GUIVerExpedientePorAlumno.xaml
	/// </summary>
	public partial class GUIVerExpedientePorAlumno : Window
    {
        private Asignacion Asignacion {get; set;}
        private Alumno Alumno { get; set; }

        public GUIVerExpedientePorAlumno(Alumno alumno)
        {
            InitializeComponent();
            Alumno = alumno;

            try
            {
                Asignacion = Alumno.CargarAsignacion();
                Asignacion.CargarDocumentos();
				LabelHorasCubiertas.Content = Asignacion.ObtenerHorasCubiertas();
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
			GridReportesMensuales.ItemsSource = Asignacion.ReportesMensuales;
			GridDocumentosDeEntregaUnica.ItemsSource = Asignacion.DocumentosDeEntregaUnica;
		}

        private void ButtonVerProyectoActual_Click(object sender, RoutedEventArgs e)
        {
            GUIVerProyectoActual verProyectoActual = new GUIVerProyectoActual(Alumno);
			Hide();
			verProyectoActual.ShowDialog();
			ShowDialog();
        }

        private void ButtonRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
