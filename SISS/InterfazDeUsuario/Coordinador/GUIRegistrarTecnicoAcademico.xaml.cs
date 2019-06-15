using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;

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
			ComboBoxCarrera.Items.Add("LIS");
			ComboBoxCarrera.Items.Add("RYSC");
			ComboBoxCarrera.Items.Add("TC");
			ComboBoxCarrera.SelectedIndex = 0;
		}

		private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
		{
			DocenteAcademico tecnicoAcademico = new DocenteAcademico
			{
				Nombre = TextBoxNombre.Text,
				CorreoElectronico = TextBoxCorreoElectronico.Text,
				Telefono = TextBoxTelefono.Text,
				Coordinador = this.Coordinador,
				Carrera = ComboBoxCarrera.SelectedValue.ToString(),
				EsActivo = true,
				Contraseña = TextBoxContraseña.Text,
				Rol = Rol.TecnicoAcademico
			};

			Mouse.OverrideCursor = Cursors.Wait;
            if (ValidarEntero(TextBoxCubiculo.Text))
            {
                tecnicoAcademico.Cubiculo = int.Parse(TextBoxCubiculo.Text);
            }
            else
            {
                tecnicoAcademico.Cubiculo = VALOR_ENTERO_MINIMO_PERMITIDO;
            }

            if (tecnicoAcademico.Validar()
                && TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text
                && TextBoxContraseña.Text == TextBoxConfirmarContraseña.Text
                && ComboBoxCarrera.SelectedIndex >= VALOR_ENTERO_MINIMO_PERMITIDO)
            {
                if (ValidarExistenciaDeCorreo(tecnicoAcademico.CorreoElectronico))
                {
                    bool registroExitoso = false;
                    try
                    {
                        tecnicoAcademico.Guardar();
                        registroExitoso = true;
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada)
                    {
                        MessageBox.Show(this, MATRICULA_DUPLICADA_MENSAJE, MATRICULA_DUPLICADA_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
                    {
                        MessageBox.Show(this, CONEXION_FALLIDA_MENSAJE, CONEXION_FALLIDA_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
                    {
                        MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
                    {
                        MessageBox.Show(this, ERROR_GUARDAR_REGISTRO, ERROR_DESCONOCIDO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
                    {
                        MessageBox.Show(this, ERROR_PETICION_MENSAJE, ERROR_INTERNO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
                    {
                        MessageBox.Show(this, ERROR_DESCONOCIDO_MENSAJE, ERROR_DESCONOCIDO_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
                    }
                    finally
                    {
                        Mouse.OverrideCursor = null;
                    }

                    if (registroExitoso)
                    {
                        MessageBox.Show("El técnico académico ha sido registrado exitosamente.", "¡Registro Exitoso!", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.None);
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show(this, "Ya existe un tecnico con ese correo.");
                }
            }
            else
            {
                Mouse.OverrideCursor = null;
                MessageBox.Show(COMPROBAR_CAMPOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
                MostrarEstadoDeValidacionNombre(TextBoxNombre);
                MostrarEstadoDeValidacionCorreoElectronico(TextBoxCorreoElectronico);
                MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
                MostrarEstadoDeValidacionContraseña(TextBoxContraseña);
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
