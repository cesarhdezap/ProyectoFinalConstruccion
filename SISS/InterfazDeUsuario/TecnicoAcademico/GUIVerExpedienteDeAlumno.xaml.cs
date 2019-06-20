using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

namespace InterfazDeUsuario.GUIsDeTecnicoAcademico
{
	/// <summary>
	/// Interaction logic for GUICapturarDocumento.xaml
	/// </summary>
	public partial class GUIVerExpedienteDeAlumno : Window
    {
		private const int NUMERO_MAXIMO_DE_REPORTES_MENSUALES = 12;
		private Asignacion Asignacion{ get; set; }
        private DocenteAcademico TecnicoAdministrativo { get; set; }

        public GUIVerExpedienteDeAlumno(DocenteAcademico tecnicoAdministrativo, Asignacion asignacion)
		{
			Mouse.OverrideCursor = Cursors.Wait;
			InitializeComponent();
			Asignacion = asignacion;
			TecnicoAdministrativo = tecnicoAdministrativo;
			CargarDocumentos();
			CargarHoras();

			if (asignacion.ReportesMensuales.Count >= NUMERO_MAXIMO_DE_REPORTES_MENSUALES)
			{
				ButtonCapturarReporteMensual.IsEnabled = false;
				ToolTip toolTip = new ToolTip
				{
					Content = MAXIMO_DE_REPORTES_MENSUALES_ENTREGADO
				};

				ButtonCapturarReporteMensual.ToolTip = toolTip;
			}

			LabelNombreDeUsuario.Content = TecnicoAdministrativo.Nombre;
			GridReportesMensuales.ItemsSource = Asignacion.ReportesMensuales;
			GridDocumentosDeEntregaUnica.ItemsSource = Asignacion.DocumentosDeEntregaUnica;
		}

		private void CargarHoras()
		{
			try
			{
				LabelHorasCubiertas.Content = Asignacion.ObtenerHorasCubiertas();
			}
			catch (AccesoADatosException e)
			{
				MostrarMessageBoxDeExcepcion(this, e);
				Close();
			}
			finally
			{
				Mouse.OverrideCursor = null;
			}
		}

		private void ButtonCapturarOtroDocumento_Click(object sender, RoutedEventArgs e)
        {
			GUICapturarOtroDocumento capturarOtroDocumento = new GUICapturarOtroDocumento(TecnicoAdministrativo, Asignacion);
			capturarOtroDocumento.ShowDialog();
			Mouse.OverrideCursor = Cursors.Wait;
			CargarDocumentos();
			GridDocumentosDeEntregaUnica.ItemsSource = Asignacion.DocumentosDeEntregaUnica;
		}

		private void ButtonCapturarReporteMensual_Click(object sender, RoutedEventArgs e)
        {
			GUIEntregarReporteMensual entregarReporteMensual = new GUIEntregarReporteMensual(TecnicoAdministrativo, Asignacion);
			entregarReporteMensual.ShowDialog();
			CargarDocumentos();
			CargarHoras();
			GridReportesMensuales.ItemsSource = Asignacion.ReportesMensuales;
        }

        private void ButtonRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

		private void CargarDocumentos()
		{
			Mouse.OverrideCursor = Cursors.Wait;

			try
			{
				Asignacion.CargarDocumentos();
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
		}
    }
}
