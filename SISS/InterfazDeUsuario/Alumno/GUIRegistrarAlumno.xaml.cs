using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LogicaDeNegocios.Excepciones;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using LogicaDeNegocios;
using System;
using LogicaDeNegocios.ClasesDominio;

namespace InterfazDeUsuario.GUIsDeAlumno
{
    public partial class GUIRegistrarAlumno : Window
    {
        public GUIRegistrarAlumno()
        {
            InitializeComponent();

			foreach (var carrera in Enum.GetValues(typeof(Carrera)))
			{
				ComboBoxCarrera.Items.Add(carrera).ToString();
			}
			ComboBoxCarrera.SelectedIndex = 0;
        }

        private void TextBoxMatricula_TextChanged(object sender, TextChangedEventArgs e)
        {
			MostrarEstadoDeValidacionMatricula(TextBoxMatricula);
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

		private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;

            Alumno alumno = new Alumno
            {
                Nombre = TextBoxNombre.Text,
                CorreoElectronico = TextBoxCorreoElectronico.Text,
                Telefono = TextBoxTelefono.Text,
                Matricula = TextBoxMatricula.Text,
                Carrera = ComboBoxCarrera.SelectedValue.ToString(),
                EstadoAlumno = EstadoAlumno.EsperandoAceptacion,
                Contraseña = TextBoxContraseña.Text
            };
			
			if (TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text 
				&& TextBoxContraseña.Text == TextBoxConfirmarContraseña.Text)
			{
				bool resultadoDeCreacion = false;

				try
				{
					if (alumno.Validar())
					{
						alumno.Guardar();
						resultadoDeCreacion = true;
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

				if (resultadoDeCreacion)
				{
                    Close();
				}
			}
			else
			{
				MostrarEstadoDeValidacionCampos();
			}
		}

		private void MostrarEstadoDeValidacionCampos()
		{
			MessageBox.Show(this, REGISTRO_EXITOSO_MENSAJE, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
			MessageBox.Show(this, COMPROBAR_CAMPOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
			MostrarEstadoDeValidacionMatricula(TextBoxMatricula);
			MostrarEstadoDeValidacionNombre(TextBoxNombre);
			MostrarEstadoDeValidacionCorreoElectronico(TextBoxCorreoElectronico);
			MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
			MostrarEstadoDeValidacionContraseña(TextBoxContraseña);
			Mouse.OverrideCursor = Cursors.Wait;

			try
			{
				MostrarEstadoDeValidacionCorreoDuplicado(TextBoxCorreoElectronico);
				MostrarEstadoDeValidacionMatriculaDuplicada(TextBoxMatricula);
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

		private void TextBoxMatricula_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				MostrarEstadoDeValidacionMatriculaDuplicada(TextBoxMatricula);
			}
			catch (AccesoADatosException ex)
			{
				MostrarMessageBoxDeExcepcion(this, ex);
			}
		}
	}
}
