using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Windows;
using InterfazDeUsuario.GUIsDeCoordinador;
using System.Windows.Input;

namespace InterfazDeUsuario.GUITipoDeSesion
{
    /// <summary>
    /// Lógica de interacción para GUICoordinador.xaml
    /// </summary>
    public partial class GUICoordinador : Window
    {
        private DocenteAcademico Coordinador;
        public GUICoordinador(Sesion sesion)
        {
            InitializeComponent();
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            try
            {
                Coordinador = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal(int.Parse(sesion.IDUsuario));
            }
            catch (FormatException e)
            {
                MessageBox.Show("Error. ID cargada incorrectamente. Mensaje: " + e.Message + " StackTrace: " + e.StackTrace.ToString());
                Close();
            }
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show("Hubo un error al completar el registro. La matricula ingresada ya existe.", "Matricula duplicada", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "Hubo un error al completar el registro. Intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException e)
            {
                MessageBox.Show("Error. No se encontro al coordinador. Mensaje: " + e.Message + " StackTrace: " + e.StackTrace.ToString());
                Close();
            }
			Mouse.OverrideCursor = null;
            LabelNombreDeUsuario.Content = Coordinador.Nombre;
        }

        private void ButtonValidarAlumno_Click(object sender, RoutedEventArgs e)
        {
            GUIValidarAlumno validarAlumno = new GUIValidarAlumno(Coordinador);
            Hide();
            validarAlumno.ShowDialog();
            ShowDialog();
        }

        private void ButtonRegistrarEncargado_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            GUIRegistrarEncargado registrarEncargado = new GUIRegistrarEncargado(Coordinador);
            registrarEncargado.ShowDialog();
            ShowDialog();
        }

        private void ButtonRegistrarOrganizacion_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistrarOrganizacion registrarOrganizacion = new GUIRegistrarOrganizacion(Coordinador);
            Hide();
            registrarOrganizacion.ShowDialog();
            ShowDialog();
        }

        private void ButtonRegistrarProyecto_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistrarProyecto registrarProyecto = new GUIRegistrarProyecto();
            Hide();
            registrarProyecto.ShowDialog();
            ShowDialog();
        }

		private void ButtonAsignarProyectosAAlumnos_Click(object sender, RoutedEventArgs e)
		{
			GUIAsignarProyectoAAlumno asignarProyectoAAlumno = new GUIAsignarProyectoAAlumno(Coordinador);
			Hide();
			asignarProyectoAAlumno.ShowDialog();
			ShowDialog();
		}

		private void ButtonBuscarProyecto_Click(object sender, RoutedEventArgs e)
		{
			GUIBuscarProyecto buscarProyecto = new GUIBuscarProyecto(Coordinador);
			Hide();
			buscarProyecto.ShowDialog();
			ShowDialog();
		}
	}
}
