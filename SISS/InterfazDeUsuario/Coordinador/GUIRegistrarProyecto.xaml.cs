using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetosAdministrador;
using System.Windows;
using System.Windows.Controls;
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

            if (indiceDeEncargado > SIN_INDICE && int.TryParse(TextBoxEstudiantesSolicitados.Text, out int cupo))
            {
                int idOrganizacion = ComboBoxOrganizacionAsociada.SelectedIndex;
                proyecto.Encargado = AdministradorDeEncargados.SeleccionarEncargadosPorIDOrganizacion(idOrganizacion)[indiceDeEncargado];
                proyecto.Cupo = cupo;
                try
                {
                    resultadoDeCreacion = proyecto.Guardar();
                }
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
                {
                    MessageBox.Show("No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos)
                {
                    MessageBox.Show("No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ObjetoNoGuardado)
                {
                    MessageBox.Show("No se guardo la Organizacion en la base de datos.");
                    resultadoDeCreacion = false;
                }

                if (resultadoDeCreacion)
                {
                    MessageBox.Show("Proyecto guardado exitosamente.");
                    Close();
                }
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
                ComboBoxEncargadoAsociado.SelectedIndex = SIN_INDICE;
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
