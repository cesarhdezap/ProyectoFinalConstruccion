using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LogicaDeNegocios.ClasesDominio;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
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

			foreach (Carrera carrera in Enum.GetValues(typeof(Carrera)))
			{
				ComboBoxCarrera.Items.Add(carrera).ToString();
			}

			ComboBoxCarrera.SelectedIndex = 0;
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
				Contraseña = TextBoxContraseña.Text,
				Rol = Rol.Coordinador
            };

            if (ValidarEntero(TextBoxCubiculo.Text))
            {
                coordinador.Cubiculo = int.Parse(TextBoxCubiculo.Text); 
            }
            else
            {
                coordinador.Cubiculo = VALOR_ENTERO_MINIMO_PERMITIDO;
            }

            if (TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text
                && TextBoxContraseña.Text == TextBoxConfirmarContraseña.Text
                && ValidarSeleccionComboBox(ComboBoxCarrera))
			{	
				bool registroExitoso = false;
                
				try
				{
                    if (coordinador.Validar())
                    {
                        coordinador.Guardar();
                        registroExitoso = true;
                    }
                    else
                    {
                        MostrarEstadoDeValidacionCampos();
                    }
				}
                catch (AccesoADatosException ex)
                {
					MostrarMessageBoxDeExcepcion(this, ex);
				}
                finally
				{
					Mouse.OverrideCursor = null;
				}

				if (registroExitoso)
				{
					MessageBox.Show(this, REGISTRO_EXITOSO_COORDINADOR, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.None);
					Close();
				}
			}
			else

            {
                Mouse.OverrideCursor = null;
                MostrarEstadoDeValidacionCampos();
            }

        }

        private void MostrarEstadoDeValidacionCampos()
        {
			Mouse.OverrideCursor = Cursors.Wait;
            MessageBox.Show(this, COMPROBAR_CAMPOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
            MostrarEstadoDeValidacionNombre(TextBoxNombre);
            MostrarEstadoDeValidacionCorreoElectronico(TextBoxCorreoElectronico);
            MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
            MostrarEstadoDeValidacionContraseña(TextBoxContraseña);
            MostrarEstadoDeValidacionCampoNumerico(TextBoxCubiculo);
			
            try
            {
                MostrarEstadoDeValidacionCorreoDuplicado(TextBoxCorreoElectronico);
            }
            catch (AccesoADatosException ex)
            {
				MostrarMessageBoxDeExcepcion(this, ex);
			}
			finally
			{
				Mouse.OverrideCursor = null;
			}
			
            MostrarEstadoDeValidacionComboBox(ComboBoxCarrera);
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

		private void TextBoxCorreoElectronico_LostFocus(object sender, RoutedEventArgs e)
		{
			Mouse.OverrideCursor = Cursors.Wait;

			try
			{
				MostrarEstadoDeValidacionCorreoDuplicado(TextBoxCorreoElectronico);
			}
			catch (AccesoADatosException ex)
			{
				MostrarMessageBoxDeExcepcion(this, ex);
			}
			finally
			{
				Mouse.OverrideCursor = null;
			}
		}	
	}
}
