using LogicaDeNegocios;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicaDeNegocios;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace InterfazDeUsuario.GUIsDeTecnicoAcademico
{
    /// <summary>
    /// Interaction logic for GUIBuscarAlumnoPorTecnicoAcademico.xaml
    /// </summary>
    public partial class GUIBuscarAlumnoPorTecnicoAcademico : Window
    {

        private AdministradorDeAlumnos AdministradorDeAlumnos {get;set;}
        private DocenteAcademico DocenteAcademico { get; set; }

        public GUIBuscarAlumnoPorTecnicoAcademico(DocenteAcademico docenteAcademico)
        {
            InitializeComponent();
            DocenteAcademico = docenteAcademico;
            LblNombreDeUsuario.Content = DocenteAcademico.Nombre;
            AdministradorDeAlumnos = new AdministradorDeAlumnos();
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                AdministradorDeAlumnos.CargarAlumnosPorCarrera(DocenteAcademico.Carrera);
            }
            catch (AccesoADatosException ex) when(ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
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
            DtgAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
        }

        private void BtnVerExpediente_Click(object sender, RoutedEventArgs e)
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

    }
}
