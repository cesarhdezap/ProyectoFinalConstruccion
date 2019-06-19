﻿using LogicaDeNegocios;
using System.Windows;
using System;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using LogicaDeNegocios.Excepciones;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Controls;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    /// <summary>
    /// Interaction logic for ValidarAlumno.xaml
    /// </summary>
    public partial class GUIValidarAlumno : Window
    {
		private AdministradorDeAlumnos AdministradorDeAlumnos { get; set; }
		private DocenteAcademico Coordinador { get; set; }
		private List<Alumno> AlumnosSeleccionados { get; set; }
		
        public GUIValidarAlumno(DocenteAcademico coordinador)
        {
            InitializeComponent();
			Coordinador = coordinador;
			AdministradorDeAlumnos = new AdministradorDeAlumnos();
			AlumnosSeleccionados = new List<Alumno>();
            LabelNombreDeUsuario.Content = coordinador.Nombre;
            ComboBoxEstadoAlumnos.Items.Add(EstadoAlumno.EsperandoAceptacion.ToString());
			ComboBoxEstadoAlumnos.Items.Add(EstadoAlumno.Rechazado.ToString());
			ComboBoxEstadoAlumnos.SelectedIndex=0; 
			Mouse.OverrideCursor = Cursors.Wait;
			try
			{
				AdministradorDeAlumnos.CargarAlumnosPorCarreraYEstado(Coordinador.Carrera, EstadoAlumno.EsperandoAceptacion);
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
			DataGridAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
		}

		private void Expander_Expanded(object sender, RoutedEventArgs e)
		{
			Mouse.OverrideCursor = Cursors.Wait;
			CambiarEstadoDeExpander(sender);
			Mouse.OverrideCursor = null;
		}

		private void Expander_Collapsed(object sender, RoutedEventArgs e)
		{
			CambiarEstadoDeExpander(sender);
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			Alumno alumnoSeleccionado = ((FrameworkElement)sender).DataContext as Alumno;
            AlumnosSeleccionados.Add(alumnoSeleccionado);
		}

		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			Alumno alumnoSeleccionado = ((FrameworkElement)sender).DataContext as Alumno;
            AlumnosSeleccionados.Remove(alumnoSeleccionado);
		}

		private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult confirmacion = MessageBox.Show(ADVERTENCIA_ACEPTACION_MENSAJE, ADVERTENCIA_TITULO, MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.Cancel);
			if (confirmacion == MessageBoxResult.OK)
			{
				Mouse.OverrideCursor = Cursors.Wait;
				List<Alumno> alumnosRechazados = AdministradorDeAlumnos.Alumnos;
				foreach (Alumno alumno in AlumnosSeleccionados)
				{
					try
					{
						alumno.Aceptar();
					}
					catch (AccesoADatosException ex)
					{
						MensajeDeErrorParaMessageBox mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox();
						mensajeDeErrorParaMessageBox = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(ex);
						MessageBox.Show(this, mensajeDeErrorParaMessageBox.Mensaje, mensajeDeErrorParaMessageBox.Titulo, MessageBoxButton.OK, MessageBoxImage.Error);
					}
					alumnosRechazados.Remove(alumno);
				}

				foreach (Alumno alumno in alumnosRechazados)
				{
					try
					{
						alumno.Rechazar();
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
				}
				MessageBox.Show(ACEPTACION_EXITOSA_MENSAJE, ACEPTACION_EXITOSA_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
			}
		}

		private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
		{
            Close();
		}

		private void ComboBoxEstadoAlumnos_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			AdministradorDeAlumnos.CargarAlumnosPorCarreraYEstado(Coordinador.Carrera, (EstadoAlumno)Enum.Parse(typeof(EstadoAlumno), ComboBoxEstadoAlumnos.SelectedItem.ToString()));
			DataGridAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
		}

		private void TextBoxBuscarProyectoPorNombre_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (TextBoxBuscarAlumno.Text == string.Empty)
			{
				DataGridAlumnos.ItemsSource = AdministradorDeAlumnos.Alumnos;
			}
			else
			{
				List<Alumno> proyectosFiltrados = AdministradorDeAlumnos.Alumnos.FindAll(delegate (Alumno alumno)
				{
					return alumno.Nombre.Contains(TextBoxBuscarAlumno.Text);
				});
				DataGridAlumnos.ItemsSource = proyectosFiltrados;
			}
		}
	}
}
