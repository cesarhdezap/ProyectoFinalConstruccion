using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    public partial class GUIRegistrarEncargado : Window
    {
        private AdministradorDeOrganizaciones AdministradorDeOrganizaciones;

        public GUIRegistrarEncargado(DocenteAcademico coordinador)
        {
            InitializeComponent();
            LabelNombreDeUsuario.Content = coordinador.Nombre;
            AdministradorDeOrganizaciones = new AdministradorDeOrganizaciones();
            AdministradorDeOrganizaciones.CargarOrganizacionesConNombre();
            ComboBoxOrganizacion.DisplayMemberPath = "Nombre";
            ComboBoxOrganizacion.ItemsSource = AdministradorDeOrganizaciones.Organizaciones;
			ComboBoxOrganizacion.SelectedIndex = 0;
        }

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
			Encargado encargado = new Encargado
			{
				Nombre = TextBoxNombre.Text,
				Puesto = TextBoxPuesto.Text,
				CorreoElectronico = TextBoxCorreoElectronico.Text,
				Telefono = TextBoxTelefono.Text
			};
				encargado.Organizacion = (Organizacion)ComboBoxOrganizacion.SelectedItem;
			if (TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text && ValidarSeleccionComboBox(ComboBoxOrganizacion))
			{
				AdministradorDeEncargados administradorDeEncargados = new AdministradorDeEncargados();

				try
				{
					encargado.Guardar();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
				{
					MessageBox.Show(this, CONEXION_FALLIDA_MENSAJE, CONEXION_FALLIDA_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
				{
					MessageBox.Show(this, ERROR_OBJETO_NO_EXISTE_MENSAJE, ERROR_OBJETO_NO_EXISTE_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
					this.Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
				{
					MessageBox.Show(this, ERROR_GUARDAR_REGISTRO, ERROR_DESCONOCIDO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
					this.Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
				{
					MessageBox.Show(this, ERROR_PETICION_MENSAJE, ERROR_INTERNO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
					this.Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
				{
					MessageBox.Show(this, ERROR_PETICION_MENSAJE, ERROR_INTERNO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
					this.Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
				{
					MessageBox.Show(this, ERROR_DESCONOCIDO_MENSAJE, ERROR_DESCONOCIDO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
					this.Close();
				}
				finally
				{
					Mouse.OverrideCursor = null;
				}
				MessageBox.Show(REGISTRO_EXITOSO_ENCARGADO, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
				Close();
			}
			else
			{
				MessageBox.Show(COMPROBAR_CAMPOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
			}
        }

        private void TextBoxNombre_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionNombre(TextBoxNombre);
        }

        private void TextBoxPuesto_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionCadena(TextBoxPuesto);
        }

        private void TextBoxCorreoElectronico_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionCorreoElectronico(TextBoxCorreoElectronico);
            TextBoxConfirmarCorreoElectronico_TextChanged(sender,e);
        }

        private void TextBoxConfirmarCorreoElectronico_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionConfirmacion(TextBoxCorreoElectronico, TextBoxConfirmarCorreoElectronico);
        }

        private void TextBoxTelefono_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

		private void ComboBoxOrganizacion_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			MostrarEstadoDeValidacionComboBox(ComboBoxOrganizacion);
		}
	}
}
