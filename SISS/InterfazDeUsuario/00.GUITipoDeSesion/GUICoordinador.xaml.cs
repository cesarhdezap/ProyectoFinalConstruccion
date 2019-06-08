using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Windows;
using InterfazDeUsuario.GUIsDeCoordinador;

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
            catch (AccesoADatosException e)
            {
                MessageBox.Show("Error. No se encontro al coordinador. Mensaje: " + e.Message + " StackTrace: " + e.StackTrace.ToString());
                Close();
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
            GUIRegistrarOrganizacion registrarOrganizacion = new GUIRegistrarOrganizacion();
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
    }
}
