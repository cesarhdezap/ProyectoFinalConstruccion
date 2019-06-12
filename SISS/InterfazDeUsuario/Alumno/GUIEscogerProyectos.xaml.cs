using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;

namespace InterfazDeUsuario.GUIsDeAlumno
{
    /// <summary>
    /// Interaction logic for GUIEscogerProyectos.xaml
    /// </summary>
    public partial class GUIEscogerProyectos : Window
    {
		private const int CANTIDAD_DE_PROYECTOS_NECESARIOS = 3;

		private AdministradorDeProyectos AdministradorDeProyectos { get; set; }
        private Solicitud Solicitud { get; set; }
        private Alumno Alumno { get; set; }

        public GUIEscogerProyectos(Alumno alumno)
        {
            InitializeComponent();
            AdministradorDeProyectos = new AdministradorDeProyectos();
            this.Alumno = alumno;
			this.Solicitud = new Solicitud(this.Alumno);
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				AdministradorDeProyectos.CargarProyectosPorEstado(EstadoProyecto.Activo);
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
			
			DataGridProyectos.ItemsSource = AdministradorDeProyectos.Proyectos;
            DataGridProyectos.UpdateLayout();
			Solicitud.Proyectos = new List<Proyecto>();
        }

        private void DtgProyectos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
			Proyecto proyectoSeleccionado = ((FrameworkElement)sender).DataContext as Proyecto;
			Solicitud.Proyectos.Add(proyectoSeleccionado);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
			Proyecto proyectoSeleccionado = ((FrameworkElement)sender).DataContext as Proyecto;
			Solicitud.Proyectos.Remove(proyectoSeleccionado);
        }

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (Solicitud.Proyectos.Count == CANTIDAD_DE_PROYECTOS_NECESARIOS)
            {
                Solicitud.Fecha = DateTime.Now;
                Mouse.OverrideCursor = Cursors.Wait;
				try
				{
					Solicitud.Guardar();
					Alumno.Solicitar();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada)
				{
					MessageBox.Show("Hubo un error al completar el registro. La matricula ingresada ya existe.", "Matricula duplicada", MessageBoxButton.OK, MessageBoxImage.Error);
					this.Close();
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
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
				{
					MessageBox.Show(this, "Hubo un error al completar el registro. Intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
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
				MessageBox.Show("Su seleccion de proyectos ha sido guardada con exito.", "¡Proyectos seleccionados exitosamente!", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.None);
				this.Close();
			}
            else
            {
                MessageBox.Show("Debe escoger 3 proyectos.", "Cantidad de proyectos seleccionados invalida", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
