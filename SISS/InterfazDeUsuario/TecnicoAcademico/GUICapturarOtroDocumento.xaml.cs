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
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using Microsoft.Win32;

namespace InterfazDeUsuario.GUIsDeTecnicoAcademico
{
    /// <summary>
    /// Interaction logic for GUICapturarOtroDocumento.xaml
    /// </summary>
    public partial class GUICapturarOtroDocumento : Window
    {
		private Asignacion Asignacion { get; set; }
		private Imagen Imagen { get; set; }
		private DocenteAcademico TecnicoAdministrativo { get; set; }
        public GUICapturarOtroDocumento(DocenteAcademico tecnicoAdministrativo, Asignacion asignacion)
        {
            InitializeComponent();
			this.TecnicoAdministrativo = tecnicoAdministrativo;
			this.Asignacion = asignacion;
        }

        private void CbxTipoDeDocumento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

		private void ButtonSeleccionarArchivo_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ventanaDeSeleccionDeArchivo = new OpenFileDialog
			{
				Filter = "Imagenes (*.jpg)|*.jpg",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};
			if (ventanaDeSeleccionDeArchivo.ShowDialog() == true)
			{
				LabelDireccionDeArchivo.Content = ventanaDeSeleccionDeArchivo.FileName;
				this.Imagen = new Imagen
				{
					DireccionDeImagen = ventanaDeSeleccionDeArchivo.FileName,
					TipoDeDocumentoEnImagen = TipoDeDocumentoEnImagen.DocumentoDeEntregaUnica
				};
			}
		}

		private void ButtonRegresar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void ButtonRegistrarDocumento_Click(object sender, RoutedEventArgs e)
		{
			DocumentoDeEntregaUnica documentoDeEntregaUnica = new DocumentoDeEntregaUnica
			{
				FechaDeEntrega = DateTime.Now,
				TipoDeDocumento = (TipoDeDocumento)ComboBoxTipoDeDocumento.SelectedIndex,
				DocenteAcademico = this.TecnicoAdministrativo,
			};
			try
			{
				documentoDeEntregaUnica.Guardar();
				Imagen.Guardar();
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
		}
	}
}
