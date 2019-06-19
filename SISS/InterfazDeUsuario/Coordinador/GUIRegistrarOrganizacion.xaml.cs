using System.Windows;
using System.Windows.Controls;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using System.Windows.Input;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    public partial class GUIRegistrarOrganizacion : Window
    {
        public GUIRegistrarOrganizacion(DocenteAcademico coordinador)
        {
            InitializeComponent();
            LabelNombreDeUsuario.Content = coordinador.Nombre;
        }

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            Organizacion organizacion = new Organizacion()
            {
                Nombre = TextBoxNombre.Text,
                Direccion = TextBoxDireccion.Text,
                Telefono = TextBoxTelefono.Text,
                CorreoElectronico = TextBoxCorreoElectronico.Text
            };

            Mouse.OverrideCursor = Cursors.Wait;
            if (organizacion.Validar() && organizacion.CorreoElectronico == TextBoxConfirmarCorreoElectronico.Text)
            {
				Mouse.OverrideCursor = Cursors.Wait;
				bool resultadoDeCreacion = false;
				try
				{
					organizacion.Guardar();
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
					MessageBox.Show(REGISTRO_EXITOSO_ORGANIZACION, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
				}
				Close();
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
			MostrarEstadoDeValidacionCadena(TextBoxDireccion);
			MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
			MostrarEstadoDeValidacionCorreoElectronico(TextBoxCorreoElectronico);
			try
			{
				MostrarEstadoDeValidacionCorreoDuplicado(TextBoxCorreoElectronico);
			}
			catch (AccesoADatosException ex)
			{
				MensajeDeErrorParaMessageBox mensajeDeErrorParaMessageBox = new MensajeDeErrorParaMessageBox();
				mensajeDeErrorParaMessageBox = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(ex);
				MessageBox.Show(this, mensajeDeErrorParaMessageBox.Mensaje, mensajeDeErrorParaMessageBox.Titulo, MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionCadena(TextBoxNombre);
        }

        private void TextBoxDireccion_TextChanged(object sender, TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionCadena(TextBoxDireccion);
		}

        private void TextBoxTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
		}

        private void TextBoxCorreoElectronico_TextChanged(object sender, TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionCorreoElectronico(TextBoxCorreoElectronico);
            TextBoxConfirmarCorreoElectronico_TextChanged(sender, e);
        }

        private void TextBoxConfirmarCorreoElectronico_TextChanged(object sender, TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionConfirmacion(TextBoxCorreoElectronico, TextBoxConfirmarCorreoElectronico);
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
