using LogicaDeNegocios.ClasesDominio;
using System;
using System.Windows;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using InterfazDeUsuario.GUIsDeTecnicoAcademico;

namespace InterfazDeUsuario.GUITipoDeSesion
{
    public partial class GUITecnicoAcademico : Window
    {
        private DocenteAcademico TecnicoAdministrativo { get; set; }

        public GUITecnicoAcademico(Sesion sesion)
        {
            InitializeComponent();

            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            TecnicoAdministrativo = new DocenteAcademico();
            Mouse.OverrideCursor = Cursors.Wait;
			try
			{
                TecnicoAdministrativo = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal(Int32.Parse(sesion.IDUsuario));
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
			LabelNombreDeUsuario.Content = TecnicoAdministrativo.Nombre;
        }

        private void ButtonBuscarAlumno_Click(object sender, RoutedEventArgs e)
        {
            GUIBuscarAlumnoPorTecnicoAcademico buscarAlumnoPorTecnicoAcademico = new GUIBuscarAlumnoPorTecnicoAcademico(TecnicoAdministrativo);
            buscarAlumnoPorTecnicoAcademico.ShowDialog();
        }
    }
}
