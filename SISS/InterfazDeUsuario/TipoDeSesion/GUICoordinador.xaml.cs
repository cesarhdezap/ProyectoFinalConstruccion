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

		private void ButtonRegistrarTecnicoAcademico_Click(object sender, RoutedEventArgs e)
		{
			GUIRegistrarTecnicoAcademico registrarTecnicoAcademico = new GUIRegistrarTecnicoAcademico(Coordinador);
			Hide();
			registrarTecnicoAcademico.ShowDialog();
			ShowDialog();
		}

		private void ButtonBuscarAlumno_Click(object sender, RoutedEventArgs e)
		{
			GUIBuscarAlumnoCoordinador buscarAlumnoCoordinador = new GUIBuscarAlumnoCoordinador(Coordinador);
			buscarAlumnoCoordinador.ShowDialog();
		}
	}
}
