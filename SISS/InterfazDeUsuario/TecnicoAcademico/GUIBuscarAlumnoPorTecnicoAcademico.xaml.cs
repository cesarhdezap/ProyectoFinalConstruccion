﻿using LogicaDeNegocios;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;
using System.Linq;

namespace InterfazDeUsuario.GUIsDeTecnicoAcademico
{
    /// <summary>
    /// Interaction logic for GUIBuscarAlumnoPorTecnicoAcademico.xaml
    /// </summary>
    public partial class GUIBuscarAlumnoPorTecnicoAcademico : Window
    {

        private AdministradorDeAlumnos AdministradorDeAlumnos {get;set;}
        private DocenteAcademico TecnicoAdministrativo { get; set; }

        public GUIBuscarAlumnoPorTecnicoAcademico(DocenteAcademico tecnicoAdministrativo)
        {
            InitializeComponent();
            TecnicoAdministrativo = tecnicoAdministrativo;
			LabelNombreDeUsuario.Content = TecnicoAdministrativo.Nombre;
            AdministradorDeAlumnos = new AdministradorDeAlumnos();
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                AdministradorDeAlumnos.CargarAlumnosPorCarreraYEstado(TecnicoAdministrativo.Carrera, EstadoAlumno.Asignado);
            }
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show("Hubo un error al completar el registro. La matricula ingresada ya existe.", "Matricula duplicada", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "Hubo un error al completar el registro. Intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
				this.Close();
			}
			Mouse.OverrideCursor = null;
			DtgAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
        }

        private void BtnVerExpediente_Click(object sender, RoutedEventArgs e)
        {
			Alumno alumnoSeleccionado = ((FrameworkElement)sender).DataContext as Alumno;
            GUIVerExpedienteDeAlumno verExpedienteDeAlumno = new GUIVerExpedienteDeAlumno(TecnicoAdministrativo, alumnoSeleccionado);
            verExpedienteDeAlumno.ShowDialog();
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            for (Visual elementoVisual = sender as Visual; elementoVisual != null; elementoVisual = VisualTreeHelper.GetParent(elementoVisual) as Visual)
                if (elementoVisual is DataGridRow fila)
                {
                    if (fila.DetailsVisibility == Visibility.Visible)
                    {
                        fila.DetailsVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        fila.DetailsVisibility = Visibility.Visible;
                    }

                    break;
                }
            Mouse.OverrideCursor = null;
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            for (var elementoVisual = sender as Visual; elementoVisual != null; elementoVisual = VisualTreeHelper.GetParent(elementoVisual) as Visual)
                if (elementoVisual is DataGridRow fila)
                {
                    if (fila.DetailsVisibility == Visibility.Visible)
                    {
                        fila.DetailsVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        fila.DetailsVisibility = Visibility.Visible;
                    }
                    break;
                }
        }

		private void TxtBuscarAlumnos_por_nombre_TextChanged(object sender, TextChangedEventArgs e)
		{
	
		}
	}
}
