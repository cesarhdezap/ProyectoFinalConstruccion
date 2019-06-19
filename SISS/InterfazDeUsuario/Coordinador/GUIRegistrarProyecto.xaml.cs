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
        private AdministradorDeOrganizaciones AdministradorDeOrganizaciones;
        private AdministradorDeEncargados AdministradorDeEncargados;

        public GUIRegistrarProyecto(DocenteAcademico coordinador)
        {
            InitializeComponent();
            LabelNombreDeUsuario.Content = coordinador.Nombre;
            AdministradorDeOrganizaciones = new AdministradorDeOrganizaciones();
            AdministradorDeEncargados = new AdministradorDeEncargados();
			Mouse.OverrideCursor = Cursors.Wait;

			try
			{
				AdministradorDeOrganizaciones.CargarOrganizaciones();
				AdministradorDeEncargados.CargarEncargadosTodos();
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

			if (AdministradorDeOrganizaciones.Organizaciones.Count > 0)
            {
                ComboBoxOrganizacionAsociada.DisplayMemberPath = "Nombre";
                ComboBoxOrganizacionAsociada.ItemsSource = AdministradorDeOrganizaciones.Organizaciones;
                ComboBoxOrganizacionAsociada.SelectedIndex = 0;
            }
            else
            {
                ComboBoxOrganizacionAsociada.IsEnabled = false;
                ComboBoxEncargadoAsociado.IsEnabled = false;
            }

            ComboBoxEncargadoAsociado.DisplayMemberPath = "Nombre";
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
					MensajeDeErrorParaMessageBox mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox();
					mensajeDeErrorParaMessageBox = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(ex);
					MessageBox.Show(this, mensajeDeErrorParaMessageBox.Mensaje, mensajeDeErrorParaMessageBox.Titulo, MessageBoxButton.OK, MessageBoxImage.Error);
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
				MessageBox.Show(COMPROBAR_CAMPOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
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
                Organizacion organizacion = ComboBoxOrganizacionAsociada.SelectedItem as Organizacion;

                List<Encargado> encargadosPorOrganizacion = AdministradorDeEncargados.SeleccionarEncargadosPorIDOrganizacion(organizacion.IDOrganizacion);
                if (encargadosPorOrganizacion.Count > 0)
                {
                    ComboBoxEncargadoAsociado.ItemsSource = encargadosPorOrganizacion;
                    ComboBoxEncargadoAsociado.SelectedIndex = 0;
                }
                else
                {
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
    }
}
