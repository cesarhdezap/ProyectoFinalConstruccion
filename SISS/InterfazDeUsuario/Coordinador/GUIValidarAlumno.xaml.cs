using LogicaDeNegocios;
using System.Windows;
using System;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    /// <summary>
    /// Interaction logic for ValidarAlumno.xaml
    /// </summary>
    public partial class GUIValidarAlumno : Window
    {
        public GUIValidarAlumno(DocenteAcademico coordinador)
        {
            InitializeComponent();
            LabelNombreDeUsuario.Content = coordinador.Nombre;

            ComboBoxEstadoDeAlumno.ItemsSource = Enum.GetValues(typeof(EstadoAlumno));
        }
    }
}
