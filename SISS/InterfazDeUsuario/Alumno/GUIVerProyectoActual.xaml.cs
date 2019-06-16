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
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.Excepciones;

namespace InterfazDeUsuario.GUIsDeAlumno
{
    /// <summary>
    /// Interaction logic for GUIVerProyectoActual.xaml
    /// </summary>
    public partial class GUIVerProyectoActual : Window
    {
		private Asignacion Asignacion { get;set; }
		private Proyecto Proyecto { get; set; }
		private Encargado Encargado { get; set; }
		private Organizacion Organizacion { get; set; }
		private DocenteAcademico Coordinador { get; set; }

		public GUIVerProyectoActual(Alumno alumno)
        {
            InitializeComponent();
            Asignacion = new Asignacion();
            Proyecto = new Proyecto();
            Encargado = new Encargado();
            Organizacion = new Organizacion();
            Coordinador = new DocenteAcademico();
            Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				Asignacion = alumno.CargarAsignacion();
				Proyecto = Asignacion.CargarProyecto();
				Encargado = Proyecto.CargarEncargado();
				Coordinador = Coordinador.CargarCoordinadorPorCarrera(alumno.Carrera);
				Organizacion = Encargado.CargarOrganizacion();
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
			AsignarValoresAInterfaz();
        }

		public GUIVerProyectoActual(DocenteAcademico coordinador, Asignacion asignacion)
		{
			InitializeComponent();
			Coordinador = coordinador;
			Asignacion = asignacion;
			Proyecto = new Proyecto();
			Encargado = new Encargado();
			Organizacion = new Organizacion();
			try
			{
				Proyecto = asignacion.CargarProyecto();
				Encargado = Proyecto.CargarEncargado();
				Organizacion = Encargado.CargarOrganizacion();
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
			AsignarValoresAInterfaz();
		}

		private void AsignarValoresAInterfaz()
		{
			LabelCorreoDelCoordinador.Content = Coordinador.CorreoElectronico;
			LabelCorreoDelEncargado.Content = Encargado.CorreoElectronico;
			LabelNombreDelCoordinador.Content = Coordinador.Nombre;
			LabelNombreDelEncargado.Content = Encargado.Nombre;
			LabelNombreDelProyecto.Content = Proyecto.Nombre;
			LabelNombreDeOrganizacion.Content = Organizacion.Nombre;
			LabelNombreDeUsuario.Content = Coordinador.Nombre;
			LabelTelefonoDelCoordinador.Content = Coordinador.Telefono;
			LabelTelefonoDelEncargado.Content = Encargado.Telefono;
			TextBoxDescripcionGeneralDeProyecto.Text = Proyecto.DescripcionGeneral;
		}

		private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
