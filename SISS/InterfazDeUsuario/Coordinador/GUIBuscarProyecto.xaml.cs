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
using LogicaDeNegocios.ObjetoAccesoDeDatos;
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
            InitializeComponent();
			LabelNombreDeUsuario.Content = coordinador.Nombre;
			AdministradorDeProyectos = new AdministradorDeProyectos();
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
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
			MessageBoxResult resultado = MessageBox.Show(CONFIRMACION_BAJA_DE_PROYECTO_MENSAJE, ADVERTENCIA_TITULO, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
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
					MensajeDeErrorParaMessageBox mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox();
					mensajeDeErrorParaMessageBox = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(ex);
					MessageBox.Show(this, mensajeDeErrorParaMessageBox.Mensaje, mensajeDeErrorParaMessageBox.Titulo, MessageBoxButton.OK, MessageBoxImage.Error);
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}
				if (proyectoDadoDeBaja)
				{
					MessageBox.Show(BAJA_DE_PROYECTO_EXITOSA_MENSAJE, OPERACION_EXITOSA_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
					AdministradorDeProyectos.Proyectos.Remove(proyectoSeleccionado);
					DataGridProyectos.ItemsSource = null;
					DataGridProyectos.ItemsSource = AdministradorDeProyectos.Proyectos;
				}
			}
		}
	}
}
