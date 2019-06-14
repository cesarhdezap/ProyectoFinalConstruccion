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
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios;
using LogicaDeNegocios.Servicios;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.Excepciones;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
namespace InterfazDeUsuario.GUIsDeDirector
{
    /// <summary>
    /// Interaction logic for GUIRegistrarCoordinador.xaml
    /// </summary>
    public partial class GUIRegistrarCoordinador : Window
    {
		private const int VALOR_DE_INDICE_SELECCIONADO_INVALIDO = -1;

		private Director Director { get; set; }
        public GUIRegistrarCoordinador(Director director)
        {
            InitializeComponent();
            this.Director = director;
            LabelNombreDeUsuario.Content = director.Nombre;
            ComboBoxCarrera.Items.Add("LIS");
            ComboBoxCarrera.Items.Add("RYSC");
            ComboBoxCarrera.Items.Add("TC");
            ComboBoxCarrera.SelectedIndex = 0;
        }

        private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarNombre(TextBoxNombre.Text))
            {
                TextBoxNombre.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxNombre.BorderBrush = Brushes.Red;
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

            if (TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text)
            {
                TextBoxConfirmarCorreoElectronico.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxConfirmarCorreoElectronico.BorderBrush = Brushes.Red;
            }
        }

        private void TextBoxConfirmarCorreoElectronico_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text)
            {
                TextBoxConfirmarCorreoElectronico.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxConfirmarCorreoElectronico.BorderBrush = Brushes.Red;
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

        private void TextBoxContraseña_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarContraseña(TextBoxContraseña.Text))
            {
                TextBoxContraseña.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxContraseña.BorderBrush = Brushes.Red;
            }
            if (TextBoxContraseña.Text == TextBoxConfirmarContraseña.Text)
            {
                TextBoxConfirmarContraseña.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxConfirmarContraseña.BorderBrush = Brushes.Red;
            }
        }

        private void TextBoxConfirmarContraseña_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxContraseña.Text == TextBoxConfirmarContraseña.Text)
            {
                TextBoxConfirmarContraseña.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxConfirmarContraseña.BorderBrush = Brushes.Red;
            }
        }

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            
			DocenteAcademico coordinador = new DocenteAcademico
			{
				Nombre = TextBoxNombre.Text,
				CorreoElectronico = TextBoxCorreoElectronico.Text,
				Telefono = TextBoxTelefono.Text,
				Coordinador = null,
				Carrera = ComboBoxCarrera.SelectedValue.ToString(),
				EsActivo = true,
				Contraseña = ServiciosDeAutenticacion.EncriptarContraseña(TextBoxContraseña.Text),
				Rol = Rol.Coordinador
            };
			Mouse.OverrideCursor = Cursors.Wait;
            if (ValidarEntero(TextBoxCubiculo.Text))
            {
                coordinador.Cubiculo = Int32.Parse(TextBoxCubiculo.Text);
                if (coordinador.Validar() && TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text && TextBoxContraseña.Text == TextBoxConfirmarContraseña.Text && ComboBoxCarrera.SelectedIndex > VALOR_DE_INDICE_SELECCIONADO_INVALIDO)
                {
					bool registroExitoso = false;
                    try
                    {

						coordinador.Guardar();
						registroExitoso = true;
                    }
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
					{
						MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexión fallida", MessageBoxButton.OK, MessageBoxImage.Error);
						this.Close();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
					{
						MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
						this.Close();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
					{
						MessageBox.Show(this, "Hubo un error al completar el registro. Intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
						this.Close();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
					{
						MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
						this.Close();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
					{
						MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
						this.Close();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
					{
						MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
						this.Close();
					}
					finally
					{
						Mouse.OverrideCursor = null;
					}
					if (registroExitoso)
					{
						MessageBox.Show("El coordinador ha sido registrado exitosamente.", "¡Registro Exitoso!", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.None);
						Close();
					}
                }
                else
                {
                    Mouse.OverrideCursor = null;
					MessageBox.Show("Porfavor compruebe los campos remarcados en rojo.", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
			else
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show("El cubiculo debe ser un valor entero no negativo y menor a 255.", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

		private void TextBoxCubiculo_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (ValidarEntero(TextBoxCubiculo.Text))
			{

				TextBoxCubiculo.BorderBrush = Brushes.Green;
			}
			else
			{
				TextBoxCubiculo.BorderBrush = Brushes.Red;
			}
		}
	}
}
