using System;
using System.Windows;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ClasesDominio;
using Microsoft.Win32;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
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
			LabelDirecciónDeArchivo.Content = MostrarVentanaDeSeleccionDeArchivos(Imagen);
		}

		private void ButtonRegistrarReporte_Click(object sender, RoutedEventArgs e)
		{
			if (ValidarCampos())
			{
				Mouse.OverrideCursor = Cursors.Wait;
				ReporteMensual reporteMensual = new ReporteMensual
				{
					FechaDeEntrega = DateTime.Now,
					NumeroDeReporte = Asignacion.ReportesMensuales.Count + 1,
					DocenteAcademico = TecnicoAdministrativo,
					Mes = (Mes)ComboBoxMes.SelectedIndex
				};

				reporteMensual.HorasReportadas = int.Parse(TextBoxHorasReportadas.Text);
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
					MostrarMessageBoxDeExcepcion(this, ex);
					Close();
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}

				if (reporteGuardado)
				{
					MessageBox.Show(this, REGISTRO_EXITOSO_REPORTE_MENSUAL, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
				}
			}
		}

		private bool ValidarCampos()
		{
			bool resultadoDeValidacion = false;

			if (Imagen.DireccionDeImagen == string.Empty)
			{
				MessageBox.Show(this, ARCHIVO_NO_SLECCIONADO_MENSAJE, ARCHIVO_NO_SLECCIONADO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if (!Asignacion.ReportesMensuales.TrueForAll(ComprobarMes))
			{
				MessageBox.Show(this, MES_DUPLICADO_MENSAJE, MES_DUPLICADO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else if (!ValidarEntero(TextBoxHorasReportadas.Text))
			{
				MessageBox.Show(this, NUMERO_DE_HORAS_INVALIDO_MENSAJE, NUMERO_DE_HORAS_INVALIDO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				resultadoDeValidacion = true;
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

		private void TextBoxHorasReportadas_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCampoNumerico(TextBoxHorasReportadas);
		}
	}
}
