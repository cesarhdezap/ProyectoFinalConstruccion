using LogicaDeNegocios;
using System.Windows;

namespace InterfazDeUsuario.GUIsDeTecnicoAcademico
{
    /// <summary>
    /// Interaction logic for GUIBuscarAlumnoPorTecnicoAcademico.xaml
    /// </summary>
    public partial class GUIBuscarAlumnoPorTecnicoAcademico : Window
    {
        DocenteAcademico TecnicoAcademico;
        public GUIBuscarAlumnoPorTecnicoAcademico(DocenteAcademico docenteAcademico)
        {
            InitializeComponent();
            TecnicoAcademico = docenteAcademico;
        }

    }
}
