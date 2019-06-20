using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using LogicaDeNegocios.ClasesDominio;
using System;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    public partial class GUIRegistrarTecnicoAcademico : Window
    {
		private DocenteAcademico Coordinador { get; set; }

        public GUIRegistrarTecnicoAcademico(DocenteAcademico coordinador)
        {
            InitializeComponent();
            Coordinador = coordinador;
			LabelNombreDeUsuario.Content = Coordinador.Nombre;
			foreach (var carrera in Enum.GetValues(typeof(Carrera)))
			{
				ComboBoxCarrera.Items.Add(carrera).ToString();
			}
			ComboBoxCarrera.SelectedIndex = 0;
		}

		private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
		{
			Mouse.OverrideCursor = Cursors.Wait;

			DocenteAcademico tecnicoAcademico = new DocenteAcademico
			{
				Nombre = TextBoxNombre.Text,
				CorreoElectronico = TextBoxCorreoElectronico.Text,
				Telefono = TextBoxTelefono.Text,
				Coordinador = Coordinador,
				Carrera = ComboBoxCarrera.SelectedValue.ToString(),
				EsActivo = true,
				Contraseña = TextBoxContraseña.Text,
				Rol = Rol.TecnicoAcademico
			};

			if (TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text
				&& TextBoxContraseña.Text == TextBoxConfirmarContraseña.Text
				&& ValidarSeleccionComboBox(ComboBoxCarrera)
				&& ValidarEntero(TextBoxCubiculo.Text))
			{
				tecnicoAcademico.Cubiculo = int.Parse(TextBoxCubiculo.Text);
				bool registroExitoso = false;

				try
				{
					if (tecnicoAcademico.Validar())
					{
						tecnicoAcademico.Guardar();
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
					MessageBox.Show(this, REGISTRO_EXITOSO_TECNICO_ACADEMICO, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.None);
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

        private void TextBoxCubiculo_TextChanged(object sender, TextChangedEventArgs e)
        {
            MostrarEstadoDeValidacionCampoNumerico(TextBoxCubiculo);
        }
    }
}
