using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetosAdministrador;
using System.Windows;
using System.Windows.Input;
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

            if (AdministradorDeOrganizaciones.Organizaciones.Count > 0)
            {
                ComboBoxOrganizacion.DisplayMemberPath = "Nombre";
                ComboBoxOrganizacion.ItemsSource = AdministradorDeOrganizaciones.Organizaciones;
                ComboBoxOrganizacion.SelectedIndex = 0;
            }
            else
            {
                ComboBoxOrganizacion.IsEnabled = false;
            }
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

			if (encargado.Validar() && TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text && ValidarSeleccionComboBox(ComboBoxOrganizacion))
			{
                Mouse.OverrideCursor = Cursors.Wait;
                encargado.Organizacion = (Organizacion)ComboBoxOrganizacion.SelectedItem;

                bool resultadoDeCreacion = false;
                try
				{
					encargado.Guardar();
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
                    MessageBox.Show(REGISTRO_EXITOSO_ENCARGADO, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
			}
			else
			{
				MessageBox.Show(COMPROBAR_CAMPOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
				MostrarEstadoDeValidacionCampos();
			}
		}

		private void MostrarEstadoDeValidacionCampos()
		{
			MostrarEstadoDeValidacionNombre(TextBoxNombre);
			MostrarEstadoDeValidacionCadena(TextBoxPuesto);
			MostrarEstadoDeValidacionCorreoElectronico(TextBoxCorreoElectronico);
			MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
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
