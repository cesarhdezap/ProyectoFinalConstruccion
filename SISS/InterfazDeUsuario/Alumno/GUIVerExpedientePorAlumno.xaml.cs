using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;

using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

namespace InterfazDeUsuario.GUIsDeAlumno
{
    /// <summary>
    /// Interaction logic for GUIVerExpedientePorAlumno.xaml
    /// </summary>
    public partial class GUIVerExpedientePorAlumno : Window
    {
        private Asignacion Asignacion {get; set;}
        private Alumno Alumno { get; set; }
        public GUIVerExpedientePorAlumno(Alumno alumno)
        {
            InitializeComponent();
            Alumno = alumno;

            try
            {
                Asignacion = Alumno.CargarAsignacion();
                Asignacion.CargarDocumentos();
				LabelHorasCubiertas.Content = Asignacion.ObtenerHorasCubiertas();
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

			
			LabelNombreDeUsuario.Content = Alumno.Nombre;
			GridReportesMensuales.ItemsSource = Asignacion.ReportesMensuales;
			GridDocumentosDeEntregaUnica.ItemsSource = Asignacion.DocumentosDeEntregaUnica;
		}

        private void ButtonVerProyectoActual_Click(object sender, RoutedEventArgs e)
        {
            GUIVerProyectoActual verProyectoActual = new GUIVerProyectoActual(Alumno);
			Hide();
			verProyectoActual.ShowDialog();
			ShowDialog();
        }

        private void ButtonRegresar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
