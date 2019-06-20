using LogicaDeNegocios.ClasesDominio;
using System.Windows;
using System.Windows.Input;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using InterfazDeUsuario.GUIsDeTecnicoAcademico;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;

namespace InterfazDeUsuario.GUITipoDeSesion
{
	public partial class GUITecnicoAcademico : Window
    {
        private DocenteAcademico TecnicoAdministrativo { get; set; }

        public GUITecnicoAcademico(Sesion sesion)
        {
			Mouse.OverrideCursor = Cursors.Wait;
            InitializeComponent();
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            TecnicoAdministrativo = new DocenteAcademico();
            
			try
			{
                TecnicoAdministrativo = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal(int.Parse(sesion.IDUsuario));
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

			LabelNombreDeUsuario.Content = TecnicoAdministrativo.Nombre;
        }

        private void ButtonBuscarAlumno_Click(object sender, RoutedEventArgs e)
        {
            GUIBuscarAlumnoPorTecnicoAcademico buscarAlumnoPorTecnicoAcademico = new GUIBuscarAlumnoPorTecnicoAcademico(TecnicoAdministrativo);
			MostrarPantalla(this, buscarAlumnoPorTecnicoAcademico);
        }

		private void LabelCerrarSesion_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Close();
		}
	}
}
