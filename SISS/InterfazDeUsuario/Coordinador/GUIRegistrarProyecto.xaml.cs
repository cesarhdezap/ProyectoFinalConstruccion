using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetosAdministrador;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using System.Collections.Generic;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    public partial class GUIRegistrarProyecto : Window
    {
        private AdministradorDeOrganizaciones AdministradorDeOrganizaciones { get; set; }
        private AdministradorDeEncargados AdministradorDeEncargados { get; set; }
		private DocenteAcademico Coordinador { get; set; }

        public GUIRegistrarProyecto(DocenteAcademico coordinador)
		{
			InitializeComponent();
			Coordinador = coordinador;
			LabelNombreDeUsuario.Content = Coordinador.Nombre;
			AdministradorDeOrganizaciones = new AdministradorDeOrganizaciones();
			AdministradorDeEncargados = new AdministradorDeEncargados();
			CargarOrganizacionesYEncargados();
			ComboBoxEncargadoAsociado.DisplayMemberPath = "Nombre";
		}

		private void CargarOrganizacionesYEncargados()
		{
			Mouse.OverrideCursor = Cursors.Wait;

			try
			{
				AdministradorDeOrganizaciones.CargarOrganizaciones();
				AdministradorDeEncargados.CargarEncargadosTodos();
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

			if (AdministradorDeOrganizaciones.Organizaciones.Count > 0)
			{
				ComboBoxOrganizacionAsociada.DisplayMemberPath = "Nombre";
				ComboBoxOrganizacionAsociada.ItemsSource = AdministradorDeOrganizaciones.Organizaciones;
				ComboBoxOrganizacionAsociada.SelectedIndex = 0;
				ComboBoxOrganizacionAsociada.IsEnabled = true;
			}
			else
			{
				ComboBoxOrganizacionAsociada.IsEnabled = false;
				ComboBoxEncargadoAsociado.IsEnabled = false;
			}
		}

		private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
		{
			Proyecto proyecto = new Proyecto()
			{
				Estado = EstadoProyecto.Activo,
				Nombre = TextBoxNombre.Text,
				ObjetivoGeneral = TextBoxObjetivoGeneral.Text,
				DescripcionGeneral = TextBoxDescripcionGeneral.Text,

			};

			proyecto.Encargado = ComboBoxEncargadoAsociado.SelectedItem as Encargado;
			Mouse.OverrideCursor = Cursors.Wait;

			if (ValidarEntero(TextBoxEstudiantesSolicitados.Text))
			{
				proyecto.Cupo = int.Parse(TextBoxEstudiantesSolicitados.Text);
			}
			else
			{
				proyecto.Cupo = 0;
			}

			if (proyecto.Validar() && ValidarSeleccionComboBox(ComboBoxEncargadoAsociado) && ValidarEntero(TextBoxEstudiantesSolicitados.Text))
			{
				bool resultadoDeCreacion = false;

				try
				{
					proyecto.Guardar();
					resultadoDeCreacion = true;
				}
				catch (AccesoADatosException ex)
				{
					MostrarMessageBoxDeExcepcion(this, ex);
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}

				if (resultadoDeCreacion)
				{
					MessageBox.Show(this, REGISTRO_EXITOSO_PROYECTO, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
					Close();
				}
			}
			else
			{
				MessageBox.Show(this, COMPROBAR_CAMPOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
				MostrarEstadoDeValidacionCampos();
				Mouse.OverrideCursor = null;
			}
		}

		private void MostrarEstadoDeValidacionCampos()
		{
			MostrarEstadoDeValidacionCadena(TextBoxNombre);
			MostrarEstadoDeValidacionCadena(TextBoxObjetivoGeneral);
			MostrarEstadoDeValidacionCadena(TextBoxDescripcionGeneral);
			MostrarEstadoDeValidacionCampoNumerico(TextBoxEstudiantesSolicitados);
			MostrarEstadoDeValidacionComboBox(ComboBoxEncargadoAsociado);
		}

		private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena(TextBoxNombre);
        }

        private void TextBoxEstudiantesSolicitados_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCampoNumerico(TextBoxEstudiantesSolicitados);
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ComboBoxOrganizacionAsociada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ValidarSeleccionComboBox(ComboBoxOrganizacionAsociada))
            {
                Organizacion organizacionSeleccionada = ComboBoxOrganizacionAsociada.SelectedItem as Organizacion;
				List<Encargado> encargadosDeOrganizacion = new List<Encargado>();
				Mouse.OverrideCursor = Cursors.Wait;

				try
				{
					encargadosDeOrganizacion = AdministradorDeEncargados.SeleccionarEncargadosPorIDOrganizacion(organizacionSeleccionada.IDOrganizacion);
				}
				catch (AccesoADatosException ex)
				{
					MostrarMessageBoxDeExcepcion(this, ex);
				}
				finally
				{
					Mouse.OverrideCursor = Cursors.Wait;
				}

				if (encargadosDeOrganizacion.Count > 0)
                {
                    ComboBoxEncargadoAsociado.ItemsSource = encargadosDeOrganizacion;
                    ComboBoxEncargadoAsociado.SelectedIndex = 0;
					ComboBoxEncargadoAsociado.IsEnabled = true;
                }
                else
                {
					ComboBoxEncargadoAsociado.SelectedItem = null;
                    ComboBoxEncargadoAsociado.IsEnabled = false;
                }
            }
        }

        private void TextBoxObjetivoGeneral_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena(TextBoxObjetivoGeneral);
        }

        private void TextBoxDescripcionGeneral_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCadena(TextBoxDescripcionGeneral);
        }

		private void ButtonRegistrarEncargado_Click(object sender, RoutedEventArgs e)
		{
			GUIRegistrarEncargado registrarEncargado = new GUIRegistrarEncargado(Coordinador);
			registrarEncargado.ShowDialog();
			CargarOrganizacionesYEncargados();
		}

		private void ButtonRegistrarOrganizacion_Click(object sender, RoutedEventArgs e)
		{
			GUIRegistrarOrganizacion registrarOrganizacion = new GUIRegistrarOrganizacion(Coordinador);
			registrarOrganizacion.ShowDialog();
			CargarOrganizacionesYEncargados();
		}

		private void ComboBoxEncargadoAsociado_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			MostrarEstadoDeValidacionComboBox(ComboBoxEncargadoAsociado);
		}
	}
}
