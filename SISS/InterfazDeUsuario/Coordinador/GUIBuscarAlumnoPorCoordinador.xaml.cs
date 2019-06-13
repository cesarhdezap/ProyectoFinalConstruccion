using LogicaDeNegocios;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;
using System.Collections.Generic;


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
            InitializeComponent();
			Coordinador = coordinador;
			LabelNombreDeUsuario.Content = Coordinador.Nombre;
			AdministradorDeAlumnos = new AdministradorDeAlumnos();
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				AdministradorDeAlumnos.CargarAlumnosPorCarreraYEstado(Coordinador.Carrera, EstadoAlumno.Asignado);
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
			for (Visual elementoVisual = sender as Visual; elementoVisual != null; elementoVisual = VisualTreeHelper.GetParent(elementoVisual) as Visual)
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
			Mouse.OverrideCursor = null;
		}

		private void Expander_Collapsed(object sender, RoutedEventArgs e)
		{
			for (var elementoVisual = sender as Visual; elementoVisual != null; elementoVisual = VisualTreeHelper.GetParent(elementoVisual) as Visual)
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
			DataGridAlumnos.ItemsSource = null;
			DataGridAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
		}

		private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
