using LogicaDeNegocios;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;
using System.Collections.Generic;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;


namespace InterfazDeUsuario.GUIsDeCoordinador
{
	/// <summary>
	/// Interaction logic for BuscarAlumnoCoordinador.xaml
	/// </summary>
	public partial class GUIBuscarAlumnoCoordinador : Window
    {
		private AdministradorDeAlumnos AdministradorDeAlumnos { get; set; }
		private DocenteAcademico Coordinador { get; set; }
		
        public GUIBuscarAlumnoCoordinador(DocenteAcademico coordinador)
        {
			Mouse.OverrideCursor = Cursors.Wait;
            InitializeComponent();
			Coordinador = coordinador;
			LabelNombreDeUsuario.Content = Coordinador.Nombre;
			AdministradorDeAlumnos = new AdministradorDeAlumnos();

			try
			{
				AdministradorDeAlumnos.CargarAlumnosPorCarreraYEstado(Coordinador.Carrera, EstadoAlumno.Asignado);
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

			DataGridAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
		}

		private void TextBoxBuscarAlumnoPorNombre_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TextBoxBuscarAlumnoPorNombre.Text == string.Empty)
			{
				DataGridAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
			}
			else
			{
				List<Alumno> alumnosFiltrados = AdministradorDeAlumnos.Alumnos.FindAll(delegate (Alumno alumno)
				{
					return alumno.Nombre.Contains(TextBoxBuscarAlumnoPorNombre.Text);
				});

				DataGridAlumnos.ItemsSource = alumnosFiltrados;
			}
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

		private void ButtonVerExpediente_Click(object sender, RoutedEventArgs e)
		{
			Alumno alumnoSeleccionado = ((FrameworkElement)sender).DataContext as Alumno;
			GUIVerExpedientePorCoordinador verExpedientePorCoordinador = new GUIVerExpedientePorCoordinador(Coordinador, alumnoSeleccionado.CargarAsignacion());
			verExpedientePorCoordinador.ShowDialog();
			Mouse.OverrideCursor = Cursors.Wait;

			try
			{
				AdministradorDeAlumnos.CargarAlumnosPorCarreraYEstado(Coordinador.Carrera, EstadoAlumno.Asignado);
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

			DataGridAlumnos.ItemsSource = null;
			DataGridAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
		}

		private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
		{
            Close();
		}
	}
}
