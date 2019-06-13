using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;

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
            InitializeComponent();
            this.Asignacion = asignacion;
            this.TecnicoAdministrativo = tecnicoAdministrativo;
            Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				Asignacion.CargarDocumentos();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
			{
				MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
			{
				MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
			{
				MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
			{
				MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
			{
				MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			finally
			{
				Mouse.OverrideCursor = null;
			}
			if (asignacion.ReportesMensuales.Count >= NUMERO_MAXIMO_DE_REPORTES_MENSUALES)
			{
				ButtonCapturarReporteMensual.IsEnabled = false;
				ToolTip toolTip = new ToolTip
				{
					Content = "El numero maximo de reportes ha sido entregado."
				};
				ButtonCapturarReporteMensual.ToolTip = toolTip;

			}
			LabelHorasCubiertas.Content = Asignacion.ObtenerHorasCubiertas();
            LabelNombreDeUsuario.Content = TecnicoAdministrativo.Nombre;
            GridReportesMensuales.ItemsSource = Asignacion.ReportesMensuales;
			GridDocumentosDeEntregaUnica.ItemsSource = Asignacion.DocumentosDeEntregaUnica;
        }

        private void ButtonCapturarOtroDocumento_Click(object sender, RoutedEventArgs e)
        {
			GUICapturarOtroDocumento capturarOtroDocumento = new GUICapturarOtroDocumento(TecnicoAdministrativo, Asignacion);
			capturarOtroDocumento.ShowDialog();
			Asignacion.CargarDocumentos();
			GridDocumentosDeEntregaUnica.ItemsSource = Asignacion.DocumentosDeEntregaUnica;
		}

		private void ButtonCapturarReporteMensual_Click(object sender, RoutedEventArgs e)
        {
			GUIEntregarReporteMensual entregarReporteMensual = new GUIEntregarReporteMensual(TecnicoAdministrativo, Asignacion);
			entregarReporteMensual.ShowDialog();
			Asignacion.CargarDocumentos();
			GridReportesMensuales.ItemsSource = Asignacion.ReportesMensuales;
			LabelHorasCubiertas.Content = Asignacion.ObtenerHorasCubiertas();
        }

        private void ButtonRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
