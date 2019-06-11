using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetosAdministrador;
using System.Windows.Input;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

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
            bool resultadoDeCreacionDeOrganizacion = false;
            if (TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text)
            {
                AdministradorDeOrganizaciones administradorDeOrganizaciones = new AdministradorDeOrganizaciones();
                try
                {
                    resultadoDeCreacionDeOrganizacion = organizacion.Guardar();
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
			}
            if (resultadoDeCreacionDeOrganizacion)
            {
                MessageBox.Show("Organizacion registrada correctamente.");
                Close();
            }
        }


        private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCadena(TextBoxNombre.Text))
            {
                TextBoxNombre.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxNombre.BorderBrush = Brushes.Red;
            }
        }

        private void TextBoxDireccion_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCadena(TextBoxDireccion.Text))
            {
                TextBoxDireccion.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxDireccion.BorderBrush = Brushes.Red;
            }
        }

        private void TextBoxTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarTelefono(TextBoxTelefono.Text))
            {
                TextBoxTelefono.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxTelefono.BorderBrush = Brushes.Red;
            }
        }

        private void TextBoxCorreoElectronico_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCorreoElectronico(TextBoxCorreoElectronico.Text))
            {
                TextBoxCorreoElectronico.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxCorreoElectronico.BorderBrush = Brushes.Red;
            }
            TextBoxConfirmarCorreoElectronico_TextChanged(sender, e);
        }

        private void TextBoxConfirmarCorreoElectronico_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxConfirmarCorreoElectronico.Text == TextBoxCorreoElectronico.Text)
            {
                TextBoxCorreoElectronico.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxCorreoElectronico.BorderBrush = Brushes.Red;
            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
