using System;
using System.Windows;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using Microsoft.Win32;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

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
					catch (AccesoADatosException ex)
					{
						MensajeDeErrorParaMessageBox mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox();
						mensajeDeErrorParaMessageBox = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(ex);
						MessageBox.Show(this, mensajeDeErrorParaMessageBox.Mensaje, mensajeDeErrorParaMessageBox.Titulo, MessageBoxButton.OK, MessageBoxImage.Error);
					}
					finally
					{
						Mouse.OverrideCursor = null;
					}
					MessageBox.Show(REGISTRO_EXITOSO_DOCUMENTO, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
				}
				else
				{
					MessageBox.Show(DOCUMENTO_YA_ENTREGAOD_MENSAJE, DOCUMENTO_YA_ENTREGAOD_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
				}
			} 
			else
			{
				MessageBox.Show(ARCHIVO_NO_SLECCIONADO_MENSAJE, ARCHIVO_NO_SLECCIONADO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
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
