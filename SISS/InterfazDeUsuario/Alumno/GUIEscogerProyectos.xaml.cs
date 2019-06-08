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
            AdministradorDeProyectos.CargarProyectosPorEstado(EstadoProyecto.Activo);
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
                if (elementoVisual is DataGridRow fila)
                { 
                    if (fila.DetailsVisibility == Visibility.Visible)
                    {
                        fila.DetailsVisibility = Visibility.Collapsed;
                    } else
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

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Solicitud.Proyectos.Add(AdministradorDeProyectos.Proyectos.ElementAt(DtgProyectos.SelectedIndex));
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Solicitud.Proyectos.Remove(AdministradorDeProyectos.Proyectos.ElementAt(DtgProyectos.SelectedIndex));
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (Solicitud.Proyectos.Count <= 3)
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
                    Mouse.OverrideCursor = null;
                }
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos )
                {
                    MessageBox.Show("No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                    Mouse.OverrideCursor = null;
                }
            }
            else
            {
                MessageBox.Show("Solo puede escoger 3 proyectos como maximo", "Demasiados proyectos seleccionados", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
