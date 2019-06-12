﻿using System;
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

namespace InterfazDeUsuario.GUIsDeTecnicoAcademico
{
	/// <summary>
	/// Interaction logic for GUIEntregarReporteMensual.xaml
	/// </summary>
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
			this.Asignacion = asignacion;
			this.TecnicoAdministrativo = tecnicoAdministrativo;
			this.Imagen = new Imagen(TipoDeDocumentoEnImagen.ReporteMensual);
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

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
				ReporteMensual reporteMensual = new ReporteMensual
				{
					FechaDeEntrega = DateTime.Now,
					NumeroDeReporte = Asignacion.ReportesMensuales.Count + 1,
					DocenteAcademico = this.TecnicoAdministrativo,
					Mes = (Mes)ComboBoxMes.SelectedIndex
				};
				reporteMensual.HorasReportadas = Int32.Parse(TextBoxHorasReportadas.Text);
				Mouse.OverrideCursor = Cursors.Wait;
				try
				{
					Asignacion.RegistrarReporteMensual(reporteMensual);
					Imagen.IDDocumento = reporteMensual.IDDocumento;
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
				MessageBox.Show("El reporte mensual fue registrado exitosamente.", "¡Registro exitoso!", MessageBoxButton.OK, MessageBoxImage.Information);
				this.Close();
			}
		}

		private bool ValidarCampos()
		{
			bool resultadoDeValidacion = false;
			if (Imagen.DireccionDeImagen != string.Empty)
			{
				if (Int32.TryParse(TextBoxHorasReportadas.Text, out int i))
				{
					if (Asignacion.ReportesMensuales.TrueForAll(ComprobarMes))
					{
						resultadoDeValidacion = true;
					}
					else
					{
						MessageBox.Show("Un reporte mensual con el mes " + ComboBoxMes.SelectedItem.ToString() + " ya fue entregado.", "Mes duplicado", MessageBoxButton.OK, MessageBoxImage.Error);
					}
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
