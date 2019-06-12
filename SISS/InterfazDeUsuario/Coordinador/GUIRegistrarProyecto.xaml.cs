using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetosAdministrador;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    public partial class GUIRegistrarProyecto : Window
    {
        private AdministradorDeOrganizaciones AdministradorDeOrganizaciones;
        private AdministradorDeEncargados AdministradorDeEncargados;
        private const int SIN_INDICE = -1;

        public GUIRegistrarProyecto(DocenteAcademico coordinador)
        {
            InitializeComponent();
            LabelNombreDeUsuario.Content = coordinador.Nombre;
            AdministradorDeOrganizaciones = new AdministradorDeOrganizaciones();
            AdministradorDeOrganizaciones.CargarOrganizaciones();
            ComboBoxOrganizacionAsociada.DisplayMemberPath = "Nombre";
            ComboBoxOrganizacionAsociada.ItemsSource = AdministradorDeOrganizaciones.Organizaciones;
			AdministradorDeEncargados = new AdministradorDeEncargados();
            AdministradorDeEncargados.CargarEncargadosTodos();
			ComboBoxEncargadoAsociado.SelectedIndex = 0;
			ComboBoxOrganizacionAsociada.SelectedIndex = 0;
		}

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            Proyecto proyecto = new Proyecto()
            {
                Estado = EstadoProyecto.Activo,
                Nombre = TextBoxNombre.Text,
                ObjetivoGeneral = TextBoxObjetivoGeneral.Text,
                DescripcionGeneral = TextBoxDescripcionGeneral.Text,
            };
            bool resultadoDeCreacion = false;
            int indiceDeEncargado = ComboBoxEncargadoAsociado.SelectedIndex;
			if (indiceDeEncargado > SIN_INDICE)
			{
				if (int.TryParse(TextBoxEstudiantesSolicitados.Text, out int cupo)) {
					int IDOrganizacion = (ComboBoxOrganizacionAsociada.SelectedItem as Organizacion).IDOrganizacion;
					proyecto.Encargado = AdministradorDeEncargados.SeleccionarEncargadosPorIDOrganizacion(IDOrganizacion)[indiceDeEncargado];
					proyecto.Cupo = cupo;
					Mouse.OverrideCursor = Cursors.Wait;
					try
					{
						resultadoDeCreacion = proyecto.Guardar();
					}
					catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeErrorDeAccesoADatos.ConexionABaseDeDatosFallida)
					{
						MessageBox.Show(this, "No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
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

					if (resultadoDeCreacion)
					{
						MessageBox.Show(this, "Proyecto guardado exitosamente.", "¡Registro exitoso!", MessageBoxButton.OK, MessageBoxImage.Information);
						Close();
					}
				} else
				{
					MessageBox.Show(this, "El cupo del proyecto debe ser un número entero.", "Cupo invalido", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
			else
			{
				MessageBox.Show(this, "Debe seleccionar un encargado para ser asociado al proyecto.", "Encargado no seleccionado", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void TextBoxEstudiantesSolicitados_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(TextBoxEstudiantesSolicitados.Text, out int resultado))
            {
                TextBoxEstudiantesSolicitados.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxEstudiantesSolicitados.BorderBrush = Brushes.Red;
            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonAdjuntarEvidencia_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboBoxOrganizacionAsociada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indiceOrganizacion = ComboBoxOrganizacionAsociada.SelectedIndex;
            if (indiceOrganizacion > SIN_INDICE)
            {
                int IDOrganizacion = AdministradorDeOrganizaciones.Organizaciones[indiceOrganizacion].IDOrganizacion;
                ComboBoxEncargadoAsociado.DisplayMemberPath = "Nombre";
                ComboBoxEncargadoAsociado.ItemsSource = AdministradorDeEncargados.SeleccionarEncargadosPorIDOrganizacion(IDOrganizacion);
                ComboBoxEncargadoAsociado.SelectedIndex = 0;
            }
        }

        private void TextBoxObjetivoGeneral_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCadena(TextBoxObjetivoGeneral.Text))
            {
                TextBoxObjetivoGeneral.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxObjetivoGeneral.BorderBrush = Brushes.Red;
            }
        }

        private void TextBoxDescripcionGeneral_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidarCadena(TextBoxDescripcionGeneral.Text))
            {
                TextBoxDescripcionGeneral.BorderBrush = Brushes.Green;
            }
            else
            {
                TextBoxDescripcionGeneral.BorderBrush = Brushes.Red;
            }
        }
    }
}
