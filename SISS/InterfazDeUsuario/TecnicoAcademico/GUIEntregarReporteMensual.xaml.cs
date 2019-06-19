using System;
using System.Windows;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ClasesDominio;
using Microsoft.Win32;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

namespace InterfazDeUsuario.GUIsDeTecnicoAcademico
{
    public partial class GUIEntregarReporteMensual : Window
	{
		private DocenteAcademico TecnicoAdministrativo { get; set; }
		private Imagen Imagen { get; set; }
		private Asignacion Asignacion { get; set; }

		public GUIEntregarReporteMensual(DocenteAcademico tecnicoAdministrativo, Asignacion asignacion)
		{
			InitializeComponent();
			ComboBoxMes.ItemsSource = Enum.GetValues(typeof(Mes));
			ComboBoxMes.SelectedIndex = 0;
            Asignacion = asignacion;
            TecnicoAdministrativo = tecnicoAdministrativo;
            Imagen = new Imagen(TipoDeDocumentoEnImagen.ReporteMensual);
		}

		private void ButtonRegresar_Click(object sender, RoutedEventArgs e)
		{
            Close();
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
                Imagen.DireccionDeImagen = ventanaDeSeleccionDeArchivo.FileName;
			}
		}

		private void ButtonRegistrarReporte_Click(object sender, RoutedEventArgs e)
		{
			if (ValidarCampos())
			{
				ReporteMensual reporteMensual = new ReporteMensual
				{
					FechaDeEntrega = DateTime.Now,
					NumeroDeReporte = Asignacion.ReportesMensuales.Count + 1,
					DocenteAcademico = TecnicoAdministrativo,
					Mes = (Mes)ComboBoxMes.SelectedIndex
				};
				reporteMensual.HorasReportadas = Int32.Parse(TextBoxHorasReportadas.Text);
				Mouse.OverrideCursor = Cursors.Wait;
				bool reporteGuardado = false;
				try
				{
					Asignacion.RegistrarReporteMensual(reporteMensual);
					Imagen.IDDocumento = reporteMensual.IDDocumento;
					Imagen.Guardar();
					reporteGuardado = true;
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
				if (reporteGuardado)
				{
					MessageBox.Show(REGISTRO_EXITOSO_REPORTE_MENSUAL, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
				}
			}
		}

		private bool ValidarCampos()
		{
			bool resultadoDeValidacion = false;
			if (Imagen.DireccionDeImagen != string.Empty)
			{
				if (ValidarEntero(TextBoxHorasReportadas.Text))
				{
					if (Asignacion.ReportesMensuales.TrueForAll(ComprobarMes))
					{
						resultadoDeValidacion = true;
					}
					else
					{
						MessageBox.Show(MES_DUPLICADO_MENSAJE, MES_DUPLICADO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
				else
				{
					MessageBox.Show(NUMERO_DE_HORAS_INVALIDO_MENSAJE, NUMERO_DE_HORAS_INVALIDO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				MessageBox.Show(ARCHIVO_NO_SLECCIONADO_MENSAJE, ARCHIVO_NO_SLECCIONADO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return resultadoDeValidacion;
		}

		private bool ComprobarMes(ReporteMensual reporteMensual)
		{
				bool resultado = true; 
				if (reporteMensual.Mes == (Mes)ComboBoxMes.SelectedIndex)
				{
					resultado = false;
				}
				return resultado;
		}
	}
}
