using LogicaDeNegocios.ClasesDominio;
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
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace InterfazDeUsuario.GUITipoDeSesion
{
    /// <summary>
    /// Interaction logic for GUIDirector.xaml
    /// </summary>
    public partial class GUIDirector : Window
    {
        private Director Director { get; set; }
        public GUIDirector(Sesion sesion)
        {
            InitializeComponent();
            DirectorDAO directorDAO = new DirectorDAO();
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                Director = directorDAO.CargarDirectorPorIDPersonal(Int32.Parse(sesion.IDUsuario));
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
            LblNombreDeUsuario.Content = Director.Nombre;
        }

        private void BtnBuscarCoordinador_Click(object sender, RoutedEventArgs e)
        {
            GUIsDeDirector.GUIBuscarCoordinador buscarCoordinador = new GUIsDeDirector.GUIBuscarCoordinador(Director);
            buscarCoordinador.ShowDialog();
        }

        private void RegistrarCoordinador_Click(object sender, RoutedEventArgs e)
        {
            GUIsDeDirector.GUIRegistrarCoordinador registrarCoordinador = new GUIsDeDirector.GUIRegistrarCoordinador(Director);
            registrarCoordinador.ShowDialog();
        }
    }
}
