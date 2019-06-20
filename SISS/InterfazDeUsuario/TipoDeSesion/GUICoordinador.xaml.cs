using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Windows;
using InterfazDeUsuario.GUIsDeCoordinador;
using System.Windows.Input;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

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
				MostrarMessageBoxDeExcepcion(this, ex);
				Close();
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
			MostrarPantalla(this, validarAlumno);
		}

        private void ButtonRegistrarEncargado_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistrarEncargado registrarEncargado = new GUIRegistrarEncargado(Coordinador);
			MostrarPantalla(this, registrarEncargado);
		}

        private void ButtonRegistrarOrganizacion_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistrarOrganizacion registrarOrganizacion = new GUIRegistrarOrganizacion(Coordinador);
			MostrarPantalla(this, registrarOrganizacion);
        }

        private void ButtonRegistrarProyecto_Click(object sender, RoutedEventArgs e)
        {
            GUIRegistrarProyecto registrarProyecto = new GUIRegistrarProyecto(Coordinador);
			MostrarPantalla(this, registrarProyecto);
		}

		private void ButtonAsignarProyectosAAlumnos_Click(object sender, RoutedEventArgs e)
		{
			GUIAsignarProyectoAAlumno asignarProyectoAAlumno = new GUIAsignarProyectoAAlumno(Coordinador);
			MostrarPantalla(this, asignarProyectoAAlumno);
		}

		private void ButtonBuscarProyecto_Click(object sender, RoutedEventArgs e)
		{
			GUIBuscarProyecto buscarProyecto = new GUIBuscarProyecto(Coordinador);
			MostrarPantalla(this, buscarProyecto);
		}

		private void ButtonRegistrarTecnicoAcademico_Click(object sender, RoutedEventArgs e)
		{
			GUIRegistrarTecnicoAcademico registrarTecnicoAcademico = new GUIRegistrarTecnicoAcademico(Coordinador);
			MostrarPantalla(this, registrarTecnicoAcademico);
		}


		private void ButtonBuscarAlumno_Click(object sender, RoutedEventArgs e)
		{
			GUIBuscarAlumnoCoordinador buscarAlumnoCoordinador = new GUIBuscarAlumnoCoordinador(Coordinador);
			MostrarPantalla(this, buscarAlumnoCoordinador);
		}

		private void LabelCerrarSesion_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Close();
		}
	}
}
