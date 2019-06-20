using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ClasesDominio;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

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
            Coordinador = coordinador;
            ReporteMensual = reporteMensual;
            TextBoxHorasReportadas.Text = ReporteMensual.HorasReportadas.ToString();
            LabelMesEnReporte.Content = ReporteMensual.Mes.ToString();

            Imagen = new Imagen(TipoDeDocumentoEnImagen.ReporteMensual)
			{
				IDDocumento = ReporteMensual.IDDocumento
			};
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
				ReporteMensual.FechaDeEntrega = DateTime.Now;
				ReporteMensual.DocenteAcademico = Coordinador;
				ReporteMensual.HorasReportadas = int.Parse(TextBoxHorasReportadas.Text);
				bool reporteActualizado = false;

				try
				{
					ReporteMensual.Actualizar();
					Imagen.Actualizar();
					reporteActualizado = true;
				}
				catch (AccesoADatosException ex)
				{
					MostrarMessageBoxDeExcepcion(this, ex);
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}

				if (reporteActualizado)
				{
					MessageBox.Show(this, ACTUALIZACION_DE_REPORTE_MENSUAL_EXITOSA_MENSAJE, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
				}
			}
			else
			{
				MostrarEstadoDeValidacionCampoNumerico(TextBoxHorasReportadas);
			}
		}

		private bool ValidarCampos()
		{
			bool resultadoDeValidacion = false;

			if (Imagen.DireccionDeImagen == string.Empty)
			{
				MessageBox.Show(this, ARCHIVO_NO_SLECCIONADO_MENSAJE, ARCHIVO_NO_SLECCIONADO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
				
			}
			else if(!ValidarEntero(TextBoxHorasReportadas.Text))
			{
				MessageBox.Show(this, NUMERO_DE_HORAS_INVALIDO_MENSAJE, NUMERO_DE_HORAS_INVALIDO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				resultadoDeValidacion = true;
			}

			return resultadoDeValidacion;
		}

		private void TextBoxHorasReportadas_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCampoNumerico(TextBoxHorasReportadas);
		}
	}
}
