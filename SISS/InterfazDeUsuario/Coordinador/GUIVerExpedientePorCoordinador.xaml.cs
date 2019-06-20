using System.Windows;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
	using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;


namespace InterfazDeUsuario.GUIsDeCoordinador
{
    public partial class GUIVerExpedientePorCoordinador : Window
    {
		private Asignacion Asignacion { get; set; }
		private DocenteAcademico Coordinador { get; set; }

		public GUIVerExpedientePorCoordinador(DocenteAcademico coordinador, Asignacion asignacion)
		{
			InitializeComponent();
			Asignacion = asignacion;
			Coordinador = coordinador;
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

			AsignarValoresAinterfaz();
		}

		private void AsignarValoresAinterfaz()
		{
			LabelHorasCubiertas.Content = Asignacion.ObtenerHorasCubiertas();
			LabelNombreDeUsuario.Content = Coordinador.Nombre;
			LabelNombreDelAlumno.Content = Asignacion.Alumno.Nombre;
			GridReportesMensuales.ItemsSource = Asignacion.ReportesMensuales;
			GridDocumentosDeEntregaUnica.ItemsSource = Asignacion.DocumentosDeEntregaUnica;
		}

		private void ButtonVerProyecto_Click(object sender, RoutedEventArgs e)
        {
			GUIsDeAlumno.GUIVerProyectoActual verProyectoActual = new GUIsDeAlumno.GUIVerProyectoActual(Coordinador, Asignacion);
			verProyectoActual.ShowDialog();
        }

        private void ButtonActualizarExpediente_Click(object sender, RoutedEventArgs e)
        {
			if (GridReportesMensuales.SelectedIndex >= 0)
			{
				ReporteMensual reporteMensualAActualizar = GridReportesMensuales.SelectedItem as ReporteMensual;
				GUIActualizarReporteMensual actualizarReporteMensual = new GUIActualizarReporteMensual(reporteMensualAActualizar, Coordinador);
				actualizarReporteMensual.ShowDialog();

				try
				{
					Asignacion.CargarDocumentos();
				}
				catch (AccesoADatosException ex)
				{
					MostrarMessageBoxDeExcepcion(this, ex);
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}

				AsignarValoresAinterfaz();
			}
			else
			{
				MessageBox.Show(this, REPORTE_NO_SELECCIONADO_MENSAJE, REPORTE_NO_SELECCIONADO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
			}
        }

        private void ButtonDarAlumnoDeBaja_Click(object sender, RoutedEventArgs e)
        {
			MessageBoxResult resultado = MessageBox.Show(this, ADVERTENCIA_BAJA_ALUMNO_MENSAJE, ADVERTENCIA_TITULO, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
			if (resultado == MessageBoxResult.Yes)
			{
				Alumno alumnoSeleccionado = Asignacion.Alumno;
				Mouse.OverrideCursor = Cursors.Wait;
				bool alumnoDadoDeBaja = false;

				try
				{
					alumnoSeleccionado.DarDeBaja();
					alumnoDadoDeBaja = true;
				}
				catch (AccesoADatosException ex)
				{
					MostrarMessageBoxDeExcepcion(this, ex);
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}

				if (alumnoDadoDeBaja)
				{
					MessageBox.Show(this, BAJA_DE_ALUMNO_EXITOSA_MENSAJE, OPERACION_EXITOSA_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
				}

                Close();
			}
		}

		private void ButtonRegresar_Click(object sender, RoutedEventArgs e)
		{
            Close();
		}
	}
}
