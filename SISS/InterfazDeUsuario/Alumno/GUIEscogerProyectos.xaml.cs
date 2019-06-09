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
        private AdministradorDeProyectos AdministradorDeProyectos { get; set; }
        private Solicitud Solicitud { get; set; }
        private Alumno Alumno { get; set; }

        public GUIEscogerProyectos(Alumno alumno)
        {
            InitializeComponent();
            AdministradorDeProyectos = new AdministradorDeProyectos();
            this.Alumno = alumno;
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
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
			Mouse.OverrideCursor = null;
			DtgProyectos.ItemsSource = AdministradorDeProyectos.Proyectos;
            DtgProyectos.UpdateLayout();
            Solicitud = new Solicitud
            {
                Proyectos = new List<Proyecto>()
            };
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

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (Solicitud.Proyectos.Count <= 3 && Solicitud.Proyectos.Count > 0)
            {
                Solicitud.Fecha = DateTime.Now;
                SolicitudDAO solicitudDAO = new SolicitudDAO();
                Mouse.OverrideCursor = Cursors.Wait;
                try
                {
                    solicitudDAO.GuardarSolicitud(Solicitud, this.Alumno);
                    Alumno.Solicitar();
                    Mouse.OverrideCursor = null;
                    MessageBoxResult messageBoxCerrada = MessageBox.Show("Su seleccion de proyectos ha sido guardada con exito.", "¡Proyectos seleccionados exitosamente!", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.None);
                    if (messageBoxCerrada == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                } 
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
                {
                    MessageBox.Show("No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos )
                {
                    MessageBox.Show("No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Mouse.OverrideCursor = null;
            }
            else
            {
                MessageBox.Show("Solo puede escoger 3 proyectos como maximo o 1 como minimo.", "Cantidad de proyectos seleccionados invalida", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
