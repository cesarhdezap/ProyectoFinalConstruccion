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
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
	/// <summary>
	/// Interaction logic for GUIAsignarProyectoAAlumno.xaml
	/// </summary>
	public partial class GUIAsignarProyectoAAlumno : Window
	{
		private DocenteAcademico Coordinador { get; set; }
		private AdministradorDeAlumnos AdministradorDeAlumnos { get; set; }
		private AdministradorDeProyectos AdministradorDeProyectos { get; set; }
		private List<Asignacion> Asignaciones;
		public List<string> NombresDeProyectos { get; set; } 

		public GUIAsignarProyectoAAlumno(DocenteAcademico coordinador)
		{
			this.DataContext = this;
			InitializeComponent();
			this.Coordinador = coordinador;
			AdministradorDeAlumnos = new AdministradorDeAlumnos();
			AdministradorDeProyectos = new AdministradorDeProyectos();
			Asignaciones = new List<Asignacion>();
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				AdministradorDeAlumnos.CargarAlumnosPorCarreraYEstado(Coordinador.Carrera, EstadoAlumno.EsperandoAsignacion);
				AdministradorDeProyectos.CargarProyectosPorEstado(EstadoProyecto.Activo);
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

			foreach (Alumno alumno in AdministradorDeAlumnos.Alumnos)
			{
				Asignacion asignacion = new Asignacion
				{
					Alumno = alumno
				};
				Asignaciones.Add(asignacion);
			}
			DataGridAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
			this.NombresDeProyectos = AdministradorDeProyectos.ObtenerNombresDeProyectos();
			Mouse.OverrideCursor = null;
		}

		private void ButtonAsignar_Click(object sender, RoutedEventArgs e)
		{
			Alumno alumnoAAsignar = ((FrameworkElement)sender).DataContext as Alumno;
			int indiceDeAlumnoAAsignar = AdministradorDeAlumnos.Alumnos.IndexOf(alumnoAAsignar);
			Asignaciones.ElementAt(indiceDeAlumnoAAsignar).FechaDeInicio = DateTime.Now;
			Asignaciones.ElementAt(indiceDeAlumnoAAsignar).HorasCubiertas = 0;
			Asignaciones.ElementAt(indiceDeAlumnoAAsignar).EstadoAsignacion = EstadoAsignacion.Activo;
			Asignaciones.ElementAt(indiceDeAlumnoAAsignar).Guardar();
		}

		private void Expander_Expanded(object sender, RoutedEventArgs e)
		{
			Mouse.OverrideCursor = Cursors.Wait;
			for (Visual elementoVisual = sender as Visual; elementoVisual != null; elementoVisual = VisualTreeHelper.GetParent(elementoVisual) as Visual)
			{
				if (elementoVisual is DataGridRow fila)
				{
					if (fila.DetailsVisibility == Visibility.Visible)
					{
						fila.DetailsVisibility = Visibility.Collapsed;
					}
					else
					{
						fila.DetailsVisibility = Visibility.Visible;
					}
					break;
				}
			}
			Mouse.OverrideCursor = null;
		}

		private void Expander_Collapsed(object sender, RoutedEventArgs e)
		{
			for (var elementoVisual = sender as Visual; elementoVisual != null; elementoVisual = VisualTreeHelper.GetParent(elementoVisual) as Visual)
			{
				if (elementoVisual is DataGridRow fila)
				{
					if (fila.DetailsVisibility == Visibility.Visible)
					{
						fila.DetailsVisibility = Visibility.Collapsed;
					}
					else
					{
						fila.DetailsVisibility = Visibility.Visible;
					}
					break;
				}
			}
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Alumno alumnoSeleccionado = ((FrameworkElement)sender).DataContext as Alumno;
			int indiceDeAlumnoSeleccionado = AdministradorDeAlumnos.Alumnos.IndexOf(alumnoSeleccionado);
			ComboBox comboBox = sender as ComboBox;
			int indiceDeProyectoSeleccionado = comboBox.SelectedIndex;
			Proyecto proyectoSeleccionado = AdministradorDeProyectos.Proyectos.ElementAt(indiceDeAlumnoSeleccionado);
			Asignaciones.ElementAt(indiceDeAlumnoSeleccionado).Proyecto = proyectoSeleccionado;
		}
	}
}
