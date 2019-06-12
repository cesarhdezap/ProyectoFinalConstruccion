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

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    /// <summary>
    /// Interaction logic for GUIVerExpedientePorCoordinador.xaml
    /// </summary>
    public partial class GUIVerExpedientePorCoordinador : Window
    {
		private Asignacion Asignacion { get; set; }
		private DocenteAcademico Coordinador { get; set; }
		public GUIVerExpedientePorCoordinador(DocenteAcademico coordinador, Asignacion asignacion)
        {
            InitializeComponent();
			this.Asignacion = asignacion;
			this.Coordinador = coordinador;
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

        }

        private void ButtonDarAlumnoDeBaja_Click(object sender, RoutedEventArgs e)
        {

        }

		private void ButtonRegresar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
