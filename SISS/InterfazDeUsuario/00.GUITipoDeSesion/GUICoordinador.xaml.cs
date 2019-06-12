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
				MessageBox.Show(this, "Hubo un error al completar la carga, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
			{
				MessageBox.Show(this, "Hubo un error al completar la carga. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
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
            GUIRegistrarProyecto registrarProyecto = new GUIRegistrarProyecto(Coordinador);
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
