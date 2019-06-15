using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    public partial class GUIRegistrarEncargado : Window
    {
        private AdministradorDeOrganizaciones AdministradorDeOrganizaciones;

        public GUIRegistrarEncargado(DocenteAcademico coordinador)
        {
            InitializeComponent();
            LabelNombreDeUsuario.Content = coordinador.Nombre;
            AdministradorDeOrganizaciones = new AdministradorDeOrganizaciones();
            AdministradorDeOrganizaciones.CargarOrganizacionesConNombre();
            ComboBoxOrganizacion.DisplayMemberPath = "Nombre";
            ComboBoxOrganizacion.ItemsSource = AdministradorDeOrganizaciones.Organizaciones;
			ComboBoxOrganizacion.SelectedIndex = 0;
        }

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
			Encargado encargado = new Encargado
			{
				Nombre = TextBoxNombre.Text,
				Puesto = TextBoxPuesto.Text,
				CorreoElectronico = TextBoxCorreoElectronico.Text,
				Telefono = TextBoxTelefono.Text
			};

			int indiceDeOrganizacion = ComboBoxOrganizacion.SelectedIndex;
			if (indiceDeOrganizacion >= 0)
			{
				encargado.Organizacion = AdministradorDeOrganizaciones.Organizaciones[indiceDeOrganizacion];
				bool resultadoDeCreacion = false;
				if (TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text)
				{
					AdministradorDeEncargados administradorDeEncargados = new AdministradorDeEncargados();
					
					try
					{
						resultadoDeCreacion = encargado.Guardar();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
					{
						MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ObjetoNoExiste)
					{
						MessageBox.Show(this, "El objeto especificado no se encontro en la base de datos.", "Objeto no encontrado", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlGuardarObjeto)
					{
						MessageBox.Show(this, "Hubo un error al completar el registro. Intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorAlConvertirObjeto)
					{
						MessageBox.Show(this, "Hubo un error al completar el registro, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.IDInvalida)
					{
						MessageBox.Show(this, "Hubo un error al completar el registro. Recarge la pagina e intentelo nuevamente, si el problema persiste, contacte a su administrador.", "Error interno", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ErrorDesconocidoDeAccesoABaseDeDatos)
					{
						MessageBox.Show(this, "No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                        Close();
					}
					finally
					{
						Mouse.OverrideCursor = null;
					}
					
				}
				if (resultadoDeCreacion)
				{
					MessageBox.Show("Encargado registrado correctamente.", "¡Registro exitoso!", MessageBoxButton.OK, MessageBoxImage.Information);
					Close();
				}
				else
				{
					MessageBox.Show("Porfavor compruebe los campos remarcados en rojo.", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				MessageBox.Show("Debe seleccionar una organización.", "Organización no seleccionada", MessageBoxButton.OK, MessageBoxImage.Error);
			}
        }

        private void TextBoxNombre_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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

        private void TextBoxPuesto_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (ValidarCadena(TextBoxPuesto.Text))
            {
                TextBoxPuesto.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxPuesto.BorderBrush = Brushes.Red;
            }
        }

        private void TextBoxCorreoElectronico_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (ValidarCorreoElectronico(TextBoxCorreoElectronico.Text))
            {
                TextBoxCorreoElectronico.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxCorreoElectronico.BorderBrush = Brushes.Red;
            }
            TextBoxConfirmarCorreoElectronico_TextChanged(sender,e);
        }

        private void TextBoxConfirmarCorreoElectronico_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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

        private void TextBoxTelefono_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
