using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
	public partial class GUIBuscarProyecto : Window
    {
		private AdministradorDeProyectos AdministradorDeProyectos { get; set; }

        public GUIBuscarProyecto(DocenteAcademico coordinador)
        {
			Mouse.OverrideCursor = Cursors.Wait;
            InitializeComponent();
			LabelNombreDeUsuario.Content = coordinador.Nombre;
			AdministradorDeProyectos = new AdministradorDeProyectos();
			
			try
			{
				AdministradorDeProyectos.CargarProyectosPorEstado(EstadoProyecto.Activo);
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

            DataGridProyectos.ItemsSource = AdministradorDeProyectos.Proyectos;
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

		private void TextBoxBuscarProyecto_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TextBoxBuscarProyecto.Text == string.Empty)
			{
				DataGridProyectos.ItemsSource = AdministradorDeProyectos.Proyectos;
			}
			else
			{
				List<Proyecto> proyectosFiltrados = AdministradorDeProyectos.Proyectos.FindAll(delegate (Proyecto proyecto)
				{
					return proyecto.Nombre.Contains(TextBoxBuscarProyecto.Text);
				});

				DataGridProyectos.ItemsSource = proyectosFiltrados;
			}
		}

		private void ButtonDarDeBaja_Click(object sender, RoutedEventArgs e)
		{
			Proyecto proyectoSeleccionado = ((FrameworkElement)sender).DataContext as Proyecto;
			MessageBoxResult resultado = MessageBox.Show(this, CONFIRMACION_BAJA_DE_PROYECTO_MENSAJE, ADVERTENCIA_TITULO, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

			if (resultado == MessageBoxResult.Yes)
			{
				bool proyectoDadoDeBaja = false;
				Mouse.OverrideCursor = Cursors.Wait;

				try
				{
					proyectoSeleccionado.DarDeBaja();
					proyectoDadoDeBaja = true;
				}
				catch (AccesoADatosException ex)
				{
					MostrarMessageBoxDeExcepcion(this, ex);
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}

				if (proyectoDadoDeBaja)
				{
					MessageBox.Show(this, BAJA_DE_PROYECTO_EXITOSA_MENSAJE, OPERACION_EXITOSA_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
					AdministradorDeProyectos.Proyectos.Remove(proyectoSeleccionado);
					DataGridProyectos.ItemsSource = null;
					DataGridProyectos.ItemsSource = AdministradorDeProyectos.Proyectos;
				}
			}
		}

		private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
