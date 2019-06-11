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
        private AdministradorDeReportesMensuales AdministradorDeReportesMensuales { get; set; }
        private AdministradorDeDocumentosDeEntregaUnica AdministradorDeDocumentosDeEntregaUnica { get; set; }
        private Alumno Alumno { get; set; }
        private DocenteAcademico DocenteAcademico { get; set; }
        public GUIVerExpedienteDeAlumno(DocenteAcademico docenteAcademico, Alumno alumno)
        {
            InitializeComponent();
            this.Alumno = alumno;
            this.DocenteAcademico = docenteAcademico;
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            Asignacion asignacion = new Asignacion();
            ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                asignacion = asignacionDAO.CargarIDsPorMatriculaDeAlumno(Alumno.Matricula).ElementAt(0);
                asignacion = asignacionDAO.CargarAsignacionPorID(asignacion.IDAsignacion);
				asignacion.CargarDocumentos();
            }
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show("Hubo un error al completar el registro. La matricula ingresada ya existe.", "Matricula duplicada", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "Hubo un error al completar el registro. Intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			Mouse.OverrideCursor = null;

			LabelHorasCubiertas.Content = asignacion.ObtenerHorasCubiertas();
            LabelNombreDeUsuario.Content = DocenteAcademico.Nombre;
            ItemsControlReportesMensuales.ItemsSource = asignacion.ReportesMensuales;
            //GrdReportesMensuales.ItemsSource = AdministradorDeReportesMensuales.ReportesMensuales;
        }

        private void BtnCapturarOtroDocumento_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCapturarReporteMensual_Click(object sender, RoutedEventArgs e)
        {
			GUIEntregarReporteMensual entregarReporteMensual = new GUIEntregarReporteMensual(DocenteAcademico, Alumno);
			entregarReporteMensual.ShowDialog();
        }

        private void BtnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
