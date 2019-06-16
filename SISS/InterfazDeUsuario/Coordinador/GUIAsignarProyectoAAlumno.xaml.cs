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
using InterfazDeUsuario.Utilerias;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

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
            DataContext = this;
			InitializeComponent();
            Coordinador = coordinador;
			LabelNombreDeUsuario.Content = coordinador.Nombre;
			AdministradorDeAlumnos = new AdministradorDeAlumnos();
			AdministradorDeProyectos = new AdministradorDeProyectos();
			Asignaciones = new List<Asignacion>();
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				AdministradorDeAlumnos.CargarAlumnosPorCarreraYEstado(Coordinador.Carrera, EstadoAlumno.EsperandoAsignacion);
				AdministradorDeProyectos.CargarProyectosPorEstado(EstadoProyecto.Activo);
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
			foreach (Alumno alumno in AdministradorDeAlumnos.Alumnos)
			{
				Asignacion asignacion = new Asignacion
				{
					Alumno = alumno
				};
				Asignaciones.Add(asignacion);
			}
			DataGridAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
            NombresDeProyectos = AdministradorDeProyectos.ObtenerNombresDeProyectos();
		}

		private void ButtonAsignar_Click(object sender, RoutedEventArgs e)
		{
			Alumno alumnoAAsignar = ((FrameworkElement)sender).DataContext as Alumno;
			int indiceDeAlumnoAAsignar = AdministradorDeAlumnos.Alumnos.IndexOf(alumnoAAsignar);
			Asignaciones.ElementAt(indiceDeAlumnoAAsignar).FechaDeInicio = DateTime.Now;
			Asignaciones.ElementAt(indiceDeAlumnoAAsignar).HorasCubiertas = 0;
			Asignaciones.ElementAt(indiceDeAlumnoAAsignar).EstadoAsignacion = EstadoAsignacion.Activo;
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				Asignaciones.ElementAt(indiceDeAlumnoAAsignar).Guardar();
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
			MessageBox.Show(this, ASIGNACION_EXITOSA_MENSAJE, ASIGNACION_EXITOSA_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
			AdministradorDeAlumnos.Alumnos.Remove(alumnoAAsignar);
			Asignaciones.RemoveAt(indiceDeAlumnoAAsignar);
			DataGridAlumnos.ItemsSource = null;
			DataGridAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
		}

		private void Expander_Expanded(object sender, RoutedEventArgs e)
		{
			Mouse.OverrideCursor = Cursors.Wait;
			CambiarEstadoDeExpander(sender);
			Mouse.OverrideCursor = null;
		}

		private void Expander_Collapsed(object sender, RoutedEventArgs e)
		{
			CambiarEstadoDeExpander(sender);
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

		private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
		{
            Close();
		}
	}
}
