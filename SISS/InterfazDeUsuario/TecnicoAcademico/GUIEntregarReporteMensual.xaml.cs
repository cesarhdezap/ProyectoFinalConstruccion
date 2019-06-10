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

namespace InterfazDeUsuario.GUIsDeTecnicoAcademico
{
	/// <summary>
	/// Interaction logic for GUIEntregarReporteMensual.xaml
	/// </summary>
	public partial class GUIEntregarReporteMensual : Window
	{
		private DocenteAcademico TecnicoAdministrativo { get; set; }
		private Alumno Alumno { get; set; }
		private Imagen Imagen { get; set; }
		private Asignacion Asignacion { get; set; }

		public GUIEntregarReporteMensual(DocenteAcademico tecnicoAdministrativo, Alumno alumno)
		{
			InitializeComponent();
			CbxMes.ItemsSource = Enum.GetValues(typeof(Mes));
			CbxMes.SelectedIndex = 0;
			this.Alumno = alumno;
			this.TecnicoAdministrativo = tecnicoAdministrativo;
			this.Asignacion = new Asignacion();
			AsignacionDAO asignacionDAO = new AsignacionDAO();
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				Asignacion = asignacionDAO.CargarIDsPorMatriculaDeAlumno(Alumno.Matricula).ElementAt(0);
				Asignacion = asignacionDAO.CargarAsignacionPorID(Asignacion.IDAsignacion);
				Asignacion.CargarDocumentos();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ObjetoNoExiste)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			Mouse.OverrideCursor = null;
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void BtnRegresar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void BtnBuscarDocumento_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ventanaDeSeleccionDeArchivo = new OpenFileDialog
			{
				Filter = "Imagenes (*.jpg)|*.jpg",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};
			if (ventanaDeSeleccionDeArchivo.ShowDialog() == true)
			{
				LblDirecciónDeArchivo.Content = ventanaDeSeleccionDeArchivo.FileName;
				this.Imagen = new Imagen
				{
					DireccionDeImagen = ventanaDeSeleccionDeArchivo.FileName,
					TipoDeDocumentoEnImagen = TipoDeDocumentoEnImagen.ReporteMensual
				};
			}
		}

		private void BtnRegistrarReporte_Click(object sender, RoutedEventArgs e)
		{
			ReporteMensual reporteMensual = new ReporteMensual
			{
				FechaDeEntrega = DateTime.Now,
				NumeroDeReporte = Asignacion.ReportesMensuales.Count + 1,
				DocenteAcademico = this.TecnicoAdministrativo,
				Mes = (Mes)CbxMes.SelectedIndex
			};
			try
			{
				reporteMensual.HorasReportadas = Int32.Parse(TxtHorasReportadas.Text);
			}
			catch (InvalidCastException ex)
			{
				MessageBox.Show("Las horas reportadas deben estar en formato de numero entero.", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			ReporteMensualDAO reporteMensualDAO = new ReporteMensualDAO();
			ImagenDAO imagenDAO = new ImagenDAO();
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				Asignacion.RegistrarReporteMensual(reporteMensual);
				Imagen.IDDocumento = reporteMensualDAO.ObtenerUltimoIDInsertado();
				imagenDAO.GuardarImagen(Imagen);
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ObjetoNoExiste)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			Mouse.OverrideCursor = null;
		}
	}
}
