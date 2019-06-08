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
            AsignacionDAO asignacionDAO = new AsignacionDAO();
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            OrganizacionDAO organizacionDAO = new OrganizacionDAO();
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();

            Asignacion asignacion = new Asignacion();
            Proyecto proyecto = new Proyecto();
            Encargado encargado = new Encargado();
            Organizacion organizacion = new Organizacion();
            DocenteAcademico coordinador = new DocenteAcademico();
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                asignacion = asignacionDAO.CargarIDsPorMatriculaDeAlumno(alumno.Matricula).ElementAt(0);
                asignacion = asignacionDAO.CargarAsignacionPorID(asignacion.IDAsignacion);
                proyecto = proyectoDAO.CargarIDProyectoPorIDAsignacion(asignacion.IDAsignacion);
                proyecto = proyectoDAO.CargarProyectoPorID(proyecto.IDProyecto);
                encargado = encargadoDAO.CargarIDPorIDProyecto(proyecto.IDProyecto);
                encargado = encargadoDAO.CargarEncargadoPorID(encargado.IDEncargado);
                coordinador = docenteAcademicoDAO.CargarIDPorCarrera(alumno.Carrera);
                coordinador = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal(coordinador.IDPersonal);
            }
            catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(this,"No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ObjetoNoExiste)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(this,"El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos)
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(this,"No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            LblCorreoDelCoordinador.Content = coordinador.CorreoElectronico;
            LblCorreoDelEncargado.Content = encargado.CorreoElectronico;
            LblNombreDelCoordinador.Content = coordinador.Nombre;
            LblNombreDelEncargado.Content = encargado.Nombre;
            LblNombreDelProyecto.Content = proyecto.Nombre;
            LblNombreDeOrganizacion.Content = organizacion.Nombre;
            LblNombreDeUsuario.Content = alumno.Nombre;
            LblTelefonoDelCoordinador.Content = coordinador.Telefono;
            LblTelefonoDelEncargado.Content = encargado.Telefono;
            TxbDescripcionGeneralDeProyecto.Text = proyecto.DescripcionGeneral;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
