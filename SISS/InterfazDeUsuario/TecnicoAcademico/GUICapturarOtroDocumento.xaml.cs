using System;
using System.Windows;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using Microsoft.Win32;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

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
			LabelDireccionDeArchivo.Content = MostrarVentanaDeSeleccionDeArchivos(Imagen);
		}

		private void ButtonRegresar_Click(object sender, RoutedEventArgs e)
		{
            Close();
		}

		private void ButtonRegistrarDocumento_Click(object sender, RoutedEventArgs e)
		{
			if (ValidarCampos())
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
					MostrarMessageBoxDeExcepcion(this, ex);
					Close();
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}

				MessageBox.Show(this, REGISTRO_EXITOSO_DOCUMENTO, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
				Close();
			}
		}

		private bool ValidarCampos()
		{
			bool resultadoDeValidacion = false;

			if (Imagen.DireccionDeImagen == string.Empty)
			{
				MessageBox.Show(this, ARCHIVO_NO_SLECCIONADO_MENSAJE, ARCHIVO_NO_SLECCIONADO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if (!Asignacion.DocumentosDeEntregaUnica.TrueForAll(ComprobarTipoDeDocumento))
			{
				MessageBox.Show(this, DOCUMENTO_YA_ENTREGADO_MENSAJE, DOCUMENTO_YA_ENTREGAOD_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
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
