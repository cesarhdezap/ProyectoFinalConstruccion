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
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;


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
            Alumno = alumno;
            Solicitud = new Solicitud(Alumno);
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
            DataGridProyectos.UpdateLayout();
			Solicitud.Proyectos = new List<Proyecto>();
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
				MessageBox.Show(SELECCION_DE_PROYECTOS_EXITOSA_MENSAJE, SELECCION_DE_PROYECTOS_EXITOSA_TITULO, MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.None);
                Close();
			}
            else
            {
                MessageBox.Show(CANTIDAD_INVALIDA_DE_PROYECTOS_SELECCIONANDOS_MENSAJE, CANTIDAD_INVALIDA_DE_PROYECTOS_SELECCIONANDOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
