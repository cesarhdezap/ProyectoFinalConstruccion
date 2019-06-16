using System.Windows;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;

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
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
			{
				MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
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
				GUIActualizarReporteMensual actualizarReporteMensual= new GUIActualizarReporteMensual(reporteMensualAActualizar, Coordinador);
				actualizarReporteMensual.ShowDialog();
				try
				{
					Asignacion.CargarDocumentos();
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
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
				{
					MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
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
				LabelHorasCubiertas.Content = Asignacion.ObtenerHorasCubiertas();
				LabelNombreDeUsuario.Content = Coordinador.Nombre;
				LabelNombreDelAlumno.Content = Asignacion.Alumno.Nombre;
				GridReportesMensuales.ItemsSource = Asignacion.ReportesMensuales;
				GridDocumentosDeEntregaUnica.ItemsSource = Asignacion.DocumentosDeEntregaUnica;

			}
			else
			{
				MessageBox.Show("Debe seleccionar un reporte mensual para actualizar.", "Ningun reporte seleccionado", MessageBoxButton.OK, MessageBoxImage.Information);
			}
        }

        private void ButtonDarAlumnoDeBaja_Click(object sender, RoutedEventArgs e)
        {
			MessageBoxResult resultado = MessageBox.Show("¿Esta seguro que desea dar de baja al alumno seleccionado? Este cambio no puede deshacerse.", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
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
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
				{
					MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
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
				if (alumnoDadoDeBaja)
				{
					MessageBox.Show(this, "El alumno fue dado de baja con exito.", "Baja exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
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
