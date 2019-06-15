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
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
			{
				MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
			{
				MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
			{
				MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
			{
				MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
			{
				MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
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
			MessageBoxResult resultado = MessageBox.Show("¿Esta seguro que desea dar de baja el proyecto seleccionado? Este cambio no puede deshacerse.", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
			if (resultado == MessageBoxResult.Yes)
			{
				bool proyectoDadoDeBaja = false;
				Mouse.OverrideCursor = Cursors.Wait;
				try
				{
					proyectoSeleccionado.DarDeBaja();
					proyectoDadoDeBaja = true;
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
				{
					MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
				{
					MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
				{
					MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
				{
					MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
				{
					MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}
				if (proyectoDadoDeBaja)
				{
					MessageBox.Show("El proyecto fue dado de baja exitosamente.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
					AdministradorDeProyectos.Proyectos.Remove(proyectoSeleccionado);
					DataGridProyectos.ItemsSource = null;
					DataGridProyectos.ItemsSource = AdministradorDeProyectos.Proyectos;
				}
			}
		}
	}
}
