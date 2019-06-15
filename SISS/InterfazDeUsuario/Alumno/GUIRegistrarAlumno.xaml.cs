using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.Servicios;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using static InterfazDeUsuario.RecursosDeTexto.MensajesAUsuario;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;
using System;
using LogicaDeNegocios.ClasesDominio;

namespace InterfazDeUsuario.GUIsDeAlumno
{
    public partial class GUIRegistrarAlumno : Window
    {
        public GUIRegistrarAlumno()
        {
            InitializeComponent();
			foreach (var carrera in Enum.GetValues(typeof(Carreras)))
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
                Contraseña = ServiciosDeAutenticacion.EncriptarContraseña(TextBoxContraseña.Text)
            };
			
			if (alumno.Validar() && TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text && TextBoxContraseña.Text == TextBoxConfirmarContraseña.Text)
			{
				if (ValidarExistenciaDeCorreo(alumno.CorreoElectronico))
				{
					if (ValidarExistenciaDeMatricula(alumno.Matricula))
					{
						bool resultadoDeCreacion = false;
						try
						{
							alumno.Guardar();
							resultadoDeCreacion = true;
						}
						catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada)
						{
							MessageBox.Show(this, MATRICULA_DUPLICADA_MENSAJE, MATRICULA_DUPLICADA_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
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
						if (resultadoDeCreacion)
						{
							MessageBox.Show(REGISTRO_EXITOSO_MENSAJE, REGISTRO_EXITOSO_TITULO, MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.None);
							this.Close();
						}
					} else
					{
						MessageBox.Show(this, MATRICULA_DUPLICADA_MENSAJE, MATRICULA_DUPLICADA_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
				else
				{
					MessageBox.Show(CORREOELECTRONICO_DUPLICADO_MENSAJE, CORREOELECTRONICO_DUPLICADO_TITULO, MessageBoxButton.OK, MessageBoxImage.Information);
				}
			}
			else
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show(COMPROBAR_CAMPOS_MENSAJE, COMPROBAR_CAMPOS_TITULO, MessageBoxButton.OK, MessageBoxImage.Error);
				MostrarEstadoDeValidacionMatricula(TextBoxMatricula);
				MostrarEstadoDeValidacionNombre(TextBoxNombre);
				MostrarEstadoDeValidacionCorreoElectronico(TextBoxCorreoElectronico);
				MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
				MostrarEstadoDeValidacionContraseña(TextBoxContraseña);
			}
		}
	}
}
