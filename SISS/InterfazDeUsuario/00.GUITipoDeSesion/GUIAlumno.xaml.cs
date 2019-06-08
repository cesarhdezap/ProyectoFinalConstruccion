using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.Excepciones;
using System.Windows;
using System.Windows.Input;

namespace InterfazDeUsuario.GUITipoDeSesion
{
    public partial class GUIAlumno : Window
    {
        private Alumno Alumno { get; set; }
        public GUIAlumno(Sesion sesion)
        {
            InitializeComponent();
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            try
            {
                Alumno = alumnoDAO.CargarAlumnoPorMatricula(sesion.IDUsuario);
            }
            catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
            {
                MessageBox.Show("No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                Mouse.OverrideCursor = null;
            }
            catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos)
            {
                MessageBox.Show("No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                Mouse.OverrideCursor = null;
            }
            if (Alumno.EstadoAlumno == EstadoAlumno.EsperandoAsignacion)
            {
                LblEsperandoAsignacion.Visibility = Visibility.Visible;
            }
            LblNombreDeUsuario.Content = Alumno.Nombre;

            LblEsperandoAsignacion.Visibility = Visibility.Hidden;
            LblDadoDeBaja.Visibility = Visibility.Hidden;
            LblLiberado.Visibility = Visibility.Hidden;
            LblEsperandoAceptacion.Visibility = Visibility.Hidden;
            BtnEscogerProyecto.Visibility = Visibility.Hidden;
            BtnVerExpediente.Visibility = Visibility.Hidden;

            switch (Alumno.EstadoAlumno)
            {
                case EstadoAlumno.EsperandoAceptacion:
                    LblEsperandoAceptacion.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.EsperandoAsignacion:
                    LblEsperandoAsignacion.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.Aceptado:
                    BtnEscogerProyecto.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.Asignado:
                    BtnVerExpediente.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.Liberado:
                    LblLiberado.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.DadoDeBaja:
                    LblDadoDeBaja.Visibility = Visibility.Visible;
                break;
                case EstadoAlumno.Rechazado:
                    LblDadoDeBaja.Visibility = Visibility.Visible;
                break;
            }
        }

        private void BtnEscogerProyecto_Click(object sender, RoutedEventArgs e)
        {
            GUIsDeAlumno.GUIEscogerProyectos escogerProyectos = new GUIsDeAlumno.GUIEscogerProyectos(Alumno);
            escogerProyectos.ShowDialog();
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                Alumno = alumnoDAO.CargarAlumnoPorMatricula(Alumno.Matricula);
            }
            catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
            {
                MessageBox.Show("No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                Mouse.OverrideCursor = null;
            }
            catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos)
            {
                MessageBox.Show("No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                Mouse.OverrideCursor = null;
            }
            if (Alumno.EstadoAlumno == EstadoAlumno.EsperandoAsignacion)
            {
                LblEsperandoAsignacion.Visibility = Visibility.Visible;
            }
        }

        private void BtnVerExpediente_Click(object sender, RoutedEventArgs e)
        {
            GUIsDeAlumno.GUIVerExpedientePorAlumno verExpediente = new GUIsDeAlumno.GUIVerExpedientePorAlumno(Alumno);
            verExpediente.ShowDialog();
        }
    }
}