using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using static LogicaDeNegocios.Servicios.ServiciosDeAutenticacion;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

namespace InterfazDeUsuario.GUIsDeDirector
{
    public partial class GUIRegistrarCoordinador : Window
    {
		private const int VALOR_DE_INDICE_SELECCIONADO_INVALIDO = -1;

		private Director Director { get; set; }
        public GUIRegistrarCoordinador(Director director)
        {
            InitializeComponent();
            Director = director;
            LabelNombreDeUsuario.Content = director.Nombre;
			foreach (var carrera in Enum.GetValues(typeof(Carreras)))
			{
				ComboBoxCarrera.Items.Add(carrera).ToString();
			}
			ComboBoxCarrera.SelectedIndex = 0;
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

        private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionNombre(TextBoxNombre);
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

        private void TextBoxTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
        }

        private void TextBoxContraseña_TextChanged(object sender, TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionContraseña(TextBoxContraseña);
			TextBoxConfirmarContraseña_TextChanged(sender, e);
        }

        private void TextBoxConfirmarContraseña_TextChanged(object sender, TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionConfirmacion(TextBoxContraseña, TextBoxConfirmarContraseña);
        }

		private void TextBoxCubiculo_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionCampoNumerico(TextBoxCubiculo);
		}

		private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
			DocenteAcademico coordinador = new DocenteAcademico
			{
				Nombre = TextBoxNombre.Text,
				CorreoElectronico = TextBoxCorreoElectronico.Text,
				Telefono = TextBoxTelefono.Text,
				Coordinador = null,
				Carrera = ComboBoxCarrera.SelectedValue.ToString(),
				EsActivo = true,
				Contraseña = EncriptarContraseña(TextBoxContraseña.Text),
				Rol = Rol.Coordinador
            };

			if (coordinador.Validar() && ValidarEntero(TextBoxCubiculo.Text) && TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text && TextBoxContraseña.Text == TextBoxConfirmarContraseña.Text && ComboBoxCarrera.SelectedIndex > VALOR_DE_INDICE_SELECCIONADO_INVALIDO)
			{	
				bool registroExitoso = false;
				try
				{
					coordinador.Cubiculo = Int32.Parse(TextBoxCubiculo.Text);
					coordinador.Guardar();
					registroExitoso = true;
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
				{
					MessageBox.Show(this, CONEXION_FALLIDA_MENSAJE, CONEXION_FALLIDA_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
				{
					MessageBox.Show(this, ERROR_GUARDAR_REGISTRO, ERROR_DESCONOCIDO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
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
				if (registroExitoso)
				{
					MessageBox.Show(REGISTRO_EXITOSO_COORDINADOR, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.None);
					Close();
				}
			}
			else
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(COMPROBAR_CAMPOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
			}

        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
	}
}
