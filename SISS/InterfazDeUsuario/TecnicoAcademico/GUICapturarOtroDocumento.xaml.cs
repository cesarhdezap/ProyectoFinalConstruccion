using System;
using System.Windows;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using Microsoft.Win32;

namespace InterfazDeUsuario.GUIsDeTecnicoAcademico
{
    public partial class GUICapturarOtroDocumento : Window
    {
		private Asignacion Asignacion { get; set; }
		private Imagen Imagen { get; set; }
		private DocenteAcademico TecnicoAdministrativo { get; set; }

        public GUICapturarOtroDocumento(DocenteAcademico tecnicoAdministrativo, Asignacion asignacion)
        {
            InitializeComponent();
            TecnicoAdministrativo = tecnicoAdministrativo;
            Asignacion = asignacion;
			Imagen = new Imagen(TipoDeDocumentoEnImagen.DocumentoDeEntregaUnica);
			ComboBoxTipoDeDocumento.ItemsSource = Enum.GetValues(typeof(TipoDeDocumento));
			ComboBoxTipoDeDocumento.SelectedIndex = 0;
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
                Imagen.DireccionDeImagen = ventanaDeSeleccionDeArchivo.FileName;
			}
		}

		private void ButtonRegresar_Click(object sender, RoutedEventArgs e)
		{
            Close();
		}

		private void ButtonRegistrarDocumento_Click(object sender, RoutedEventArgs e)
		{
			if (Imagen.DireccionDeImagen != string.Empty)
			{
				if (Asignacion.DocumentosDeEntregaUnica.TrueForAll(ComprobarTipoDeDocumento))
				{
					DocumentoDeEntregaUnica documentoDeEntregaUnica = new DocumentoDeEntregaUnica
					{
						FechaDeEntrega = DateTime.Now,
						TipoDeDocumento = (TipoDeDocumento)ComboBoxTipoDeDocumento.SelectedIndex,
						DocenteAcademico = TecnicoAdministrativo,
					};
					try
					{
						Asignacion.RegistrarDocumentoDeEntregaUnica(documentoDeEntregaUnica);
						Imagen.IDDocumento = documentoDeEntregaUnica.IDDocumento;
						Imagen.Guardar();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
					{
						MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
					{
						MessageBox.Show(this, "Hubo un error al completar el registro. Intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
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
					MessageBox.Show("El " + ComboBoxTipoDeDocumento.SelectedItem.ToString() + " fue registrado exitosamente.", "¡Registro exitoso!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
				}
				else
				{
					MessageBox.Show("El documento " + ComboBoxTipoDeDocumento.SelectedItem.ToString() + " ya fue entregado.", "Documento ya entregado", MessageBoxButton.OK, MessageBoxImage.Information);
				}
			} 
			else
			{
				MessageBox.Show("Debe seleccionar un archivo para continuar.", "Archivo no seleccionado", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private bool ComprobarTipoDeDocumento(DocumentoDeEntregaUnica documentoDeEntregaUnica)
		{
			bool resultado = true;
			if (documentoDeEntregaUnica.TipoDeDocumento == (TipoDeDocumento)ComboBoxTipoDeDocumento.SelectedIndex)
			{
				resultado = false;
			}
			return resultado;
		}
	}
}
