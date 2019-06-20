using LogicaDeNegocios;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using System.Collections.Generic;

namespace InterfazDeUsuario.GUIsDeTecnicoAcademico
{
	public partial class GUIBuscarAlumnoPorTecnicoAcademico : Window
    {
        private AdministradorDeAlumnos AdministradorDeAlumnos { get; set; }
        private DocenteAcademico TecnicoAdministrativo { get; set; }

        public GUIBuscarAlumnoPorTecnicoAcademico(DocenteAcademico tecnicoAdministrativo)
        {
			Mouse.OverrideCursor = Cursors.Wait;
            InitializeComponent();
            TecnicoAdministrativo = tecnicoAdministrativo;
			LabelNombreDeUsuario.Content = TecnicoAdministrativo.Nombre;
            AdministradorDeAlumnos = new AdministradorDeAlumnos();
            
            try
            {
                AdministradorDeAlumnos.CargarAlumnosPorCarreraYEstado(TecnicoAdministrativo.Carrera, EstadoAlumno.Asignado);
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

        private void BtnVerExpediente_Click(object sender, RoutedEventArgs e)
        {
			Alumno alumnoSeleccionado = ((FrameworkElement)sender).DataContext as Alumno;
            GUIVerExpedienteDeAlumno verExpedienteDeAlumno = new GUIVerExpedienteDeAlumno(TecnicoAdministrativo, alumnoSeleccionado.CargarAsignacion());
			Hide();
            verExpedienteDeAlumno.ShowDialog();
			ShowDialog();
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

		private void TextBoxBuscarAlumnosPorNombre_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TextBoxBuscarAlumnosPorNombre.Text == string.Empty)
			{
				DataGridAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
			}
			else
			{
				List<Alumno> alumnosFiltrados = AdministradorDeAlumnos.Alumnos.FindAll(delegate (Alumno alumno)
				{
					return alumno.Nombre.Contains(TextBoxBuscarAlumnosPorNombre.Text);
				});
				DataGridAlumnos.ItemsSource = alumnosFiltrados;
			}
		}

		private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
		{
            Close();
		}
	}
}
