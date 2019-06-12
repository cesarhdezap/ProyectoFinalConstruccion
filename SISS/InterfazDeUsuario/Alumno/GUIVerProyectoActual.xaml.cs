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
using LogicaDeNegocios.Excepciones;

namespace InterfazDeUsuario.GUIsDeAlumno
{
    /// <summary>
    /// Interaction logic for GUIVerProyectoActual.xaml
    /// </summary>
    public partial class GUIVerProyectoActual : Window
    {
        public GUIVerProyectoActual(Alumno alumno)
        {
            InitializeComponent();
            Asignacion asignacion = new Asignacion();
            Proyecto proyecto = new Proyecto();
            Encargado encargado = new Encargado();
            Organizacion organizacion = new Organizacion();
            DocenteAcademico coordinador = new DocenteAcademico();
            Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				asignacion = alumno.CargarAsignacion();
				proyecto = asignacion.CargarProyecto();
				encargado = proyecto.CargarEncargado();
				coordinador = coordinador.CargarCoordinadorPorCarrera();
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
			LabelCorreoDelCoordinador.Content = coordinador.CorreoElectronico;
            LabelCorreoDelEncargado.Content = encargado.CorreoElectronico;
            LabelNombreDelCoordinador.Content = coordinador.Nombre;
            LabelNombreDelEncargado.Content = encargado.Nombre;
            LabelNombreDelProyecto.Content = proyecto.Nombre;
            LabelNombreDeOrganizacion.Content = organizacion.Nombre;
            LabelNombreDeUsuario.Content = alumno.Nombre;
            LabelTelefonoDelCoordinador.Content = coordinador.Telefono;
            LabelTelefonoDelEncargado.Content = encargado.Telefono;
            TextBoxDescripcionGeneralDeProyecto.Text = proyecto.DescripcionGeneral;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
