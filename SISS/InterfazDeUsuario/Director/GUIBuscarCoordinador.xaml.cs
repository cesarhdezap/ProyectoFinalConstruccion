using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;
using System.Collections.Generic;

namespace InterfazDeUsuario.GUIsDeDirector
{
	public partial class GUIBuscarCoordinador : Window
	{
		private Director Director { get; set; }
		private AdministradorDeDocentesAcademicos AdministradorDeDocentesAcademicos { get; set; }
        public GUIBuscarCoordinador(Director director)
        {
            InitializeComponent();
            this.Director = director;
			AdministradorDeDocentesAcademicos = new AdministradorDeDocentesAcademicos();
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				AdministradorDeDocentesAcademicos.CargarDocentesPorRol(Rol.Coordinador);
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
			{
				MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
			{
				MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
			{
				MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
			{
				MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
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
			this.LabelNombreDeUsuario.Content = director.Nombre;
			this.DataGridCoordinadores.ItemsSource = AdministradorDeDocentesAcademicos.DocentesAcademicos;
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

		private void TextBoxBuscarCoordinadorPorNombre_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TextBoxBuscarCoordinadorPorNombre.Text == string.Empty)
			{
				DataGridCoordinadores.ItemsSource = AdministradorDeDocentesAcademicos.DocentesAcademicos;
			}
			else
			{
				List<DocenteAcademico> coordinadoresFiltrados = AdministradorDeDocentesAcademicos.DocentesAcademicos.FindAll(delegate (DocenteAcademico coordinador)
				{
					return coordinador.Nombre.Contains(TextBoxBuscarCoordinadorPorNombre.Text);
				});
				DataGridCoordinadores.ItemsSource = coordinadoresFiltrados;
			}
		}
	}
}
