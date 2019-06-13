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
using LogicaDeNegocios.ClasesDominio;
using System.IO;
using Microsoft.Win32;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    /// <summary>
    /// Interaction logic for GUIActualizarReporteMensual.xaml
    /// </summary>
    public partial class GUIActualizarReporteMensual : Window
    {
		private DocenteAcademico Coordinador { get; set; }
		private Imagen Imagen { get; set; }
		private ReporteMensual ReporteMensual { get; set; }
		public GUIActualizarReporteMensual(ReporteMensual reporteMensual, DocenteAcademico coordinador)
        {
            InitializeComponent();
			this.Coordinador = coordinador;
			this.ReporteMensual = reporteMensual;
			this.TextBoxHorasReportadas.Text = ReporteMensual.HorasReportadas.ToString();
			this.LabelMesEnReporte.Content = ReporteMensual.Mes.ToString();
			this.Imagen = new Imagen(TipoDeDocumentoEnImagen.ReporteMensual)
			{
				IDDocumento = ReporteMensual.IDDocumento
			};
        }

		private void ButtonRegresar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void ButtonBuscarDocumento_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ventanaDeSeleccionDeArchivo = new OpenFileDialog
			{
				Filter = "Imagenes (*.jpg)|*.jpg",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};
			if (ventanaDeSeleccionDeArchivo.ShowDialog() == true)
			{
				LabelDirecciónDeArchivo.Content = ventanaDeSeleccionDeArchivo.FileName;
				this.Imagen.DireccionDeImagen = ventanaDeSeleccionDeArchivo.FileName;
			}
		}

		private void ButtonRegistrarReporte_Click(object sender, RoutedEventArgs e)
		{
			if (ValidarCampos())
			{
				ReporteMensual.FechaDeEntrega = DateTime.Now;
				ReporteMensual.DocenteAcademico = Coordinador;
				ReporteMensual.HorasReportadas = Int32.Parse(TextBoxHorasReportadas.Text);
				bool reporteActualizado = false;
				try
				{
					ReporteMensual.Actualizar();
					Imagen.Actualizar();
					reporteActualizado = true;
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
				{
					MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
					this.Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
				{
					MessageBox.Show(this, "Hubo un error al completar el registro. Intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
					this.Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
				{
					MessageBox.Show(this, "Hubo un error al completar el la carga, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
					this.Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
				{
					MessageBox.Show(this, "Hubo un error al completar el la carga. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
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
				if (reporteActualizado)
				{
					MessageBox.Show("El reporte mensual fue actualizado exitosamente.", "¡Registro exitoso!", MessageBoxButton.OK, MessageBoxImage.Information);
					this.Close();
				}
			}
		}

		private bool ValidarCampos()
		{
			bool resultadoDeValidacion = false;
			if (Imagen.DireccionDeImagen != string.Empty)
			{
				if (Int32.TryParse(TextBoxHorasReportadas.Text, out int i))
				{
					resultadoDeValidacion = true;
				}
				else
				{
					MessageBox.Show("El número de horas reportadas debe ser un valor entero.", "Número de horas invalido", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				MessageBox.Show("Debe seleccionar un archivo para continuar.", "Archivo no seleccionado", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return resultadoDeValidacion;
		}
	}
}
