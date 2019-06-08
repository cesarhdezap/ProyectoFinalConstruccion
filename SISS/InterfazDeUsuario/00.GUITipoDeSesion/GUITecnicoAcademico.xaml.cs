
using LogicaDeNegocios;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using System;
using System.Windows;

namespace InterfazDeUsuario.GUITipoDeSesion
{
    /// <summary>
    /// Interaction logic for GUITecnicoAcademico.xaml
    /// </summary>
    public partial class GUITecnicoAcademico : Window
    {
        private DocenteAcademico TecnicoAcademico;

        public GUITecnicoAcademico(Sesion sesion)
        {
            InitializeComponent();
            DocenteAcademicoDAO docenteAcademicoDAO = new DocenteAcademicoDAO();
            try
            {
                TecnicoAcademico = docenteAcademicoDAO.CargarDocenteAcademicoPorIDPersonal(int.Parse(sesion.IDUsuario));
            }
            catch (FormatException e)
            {
                MessageBox.Show("Error. ID cargada incorrectamente. Mensaje: " + e.Message + " StackTrace: " + e.StackTrace.ToString());
                Close();
            }
            catch(AccesoADatosException e)
            {
                MessageBox.Show("Error. No se encontro al tecnico. Mensaje: " + e.Message + " StackTrace: " + e.StackTrace.ToString());
                Close();
            }

            LabelNombreDeUsuario.Content = TecnicoAcademico.Nombre;
        }

        private void ButtonBuscarAlumno_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
