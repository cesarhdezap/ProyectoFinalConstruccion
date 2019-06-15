using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios;
using LogicaDeNegocios.Servicios;
using static InterfazDeUsuario.Utilerias.UtileriasDeElementosGraficos;
using InterfazDeUsuario;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace InterfazDeUsuario.GUIsDeAlumno
{
    /// <summary>
    /// Interaction logic for GUIRegistrarAlumno.xaml
    /// </summary>
    public partial class GUIRegistrarAlumno : Window
    {
        public GUIRegistrarAlumno()
        {
            InitializeComponent();
            ComboBoxCarrera.Items.Add("LIS");
            ComboBoxCarrera.Items.Add("RYSC");
            ComboBoxCarrera.Items.Add("TC");
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
			MostrarEstadoDeValidacionCorreoElectronico(TextBoxConfirmarCorreoElectronico);
			TextBoxConfirmarCorreoElectronico_TextChanged(sender, e);
		}

        private void TextBoxConfirmarCorreoElectronico_TextChanged(object sender, TextChangedEventArgs e)
		{
			MostrarEstadoDeValidacionConfirmacion(TextBoxConfirmarCorreoElectronico, TextBoxConfirmarCorreoElectronico);
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
			bool resultadoDeCreacion = false;
			if (alumno.Validar() && TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text && TextBoxContraseña.Text == TextBoxConfirmarContraseña.Text)
			{
				Mouse.OverrideCursor = Cursors.Wait;
				try
				{
					alumno.Guardar();
					resultadoDeCreacion = true;
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.InsercionFallidaPorLlavePrimariDuplicada)
				{
					MessageBox.Show("Hubo un error al completar el registro. La matricula ingresada ya existe.", "Matricula duplicada", MessageBoxButton.OK, MessageBoxImage.Error);
					this.Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
				{
					MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
					this.Close();
				}
				catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
				{
					MessageBox.Show(this, "Hubo un error al completar el registro. Intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
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
				if (resultadoDeCreacion)
				{
					MessageBox.Show("Ha sido registrado exitosamente.", "¡Registro Exitoso!", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.OK, MessageBoxOptions.None);
					this.Close();
				}
			}
			else
			{
				Mouse.OverrideCursor = null;
				MessageBox.Show("Porfavor compruebe los campos remarcados en rojo.", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
				MostrarEstadoDeValidacionMatricula(TextBoxMatricula);
				MostrarEstadoDeValidacionNombre(TextBoxNombre);
				MostrarEstadoDeValidacionCorreoElectronico(TextBoxCorreoElectronico);
				MostrarEstadoDeValidacionTelefono(TextBoxTelefono);
				MostrarEstadoDeValidacionContraseña(TextBoxContraseña);
			}
		}
	}
}
