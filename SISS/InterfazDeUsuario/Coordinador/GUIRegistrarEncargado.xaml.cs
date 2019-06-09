using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;
using LogicaDeNegocios.ObjetosAdministrador;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    public partial class GUIRegistrarEncargado : Window
    {
        private List<Organizacion> Organizaciones;

        public GUIRegistrarEncargado(DocenteAcademico coordinador)
        {
            InitializeComponent();
            LabelNombreDeUsuario.Content = coordinador.Nombre;

            OrganizacionDAO organizacionDAO = new OrganizacionDAO();
            Organizaciones = organizacionDAO.CargarIDYNombreDeOrganizaciones();

            ComboBoxOrganizacion.DisplayMemberPath = "Nombre";
            ComboBoxOrganizacion.ItemsSource = Organizaciones;

        }

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            Encargado encargado = new Encargado();
            encargado.Nombre = TextBoxNombre.Text;
            encargado.Puesto = TextBoxPuesto.Text;
            encargado.CorreoElectronico = TextBoxCorreoElectronico.Text;
            encargado.Telefono = TextBoxTelefono.Text;

            int indiceDeOrganizacion = ComboBoxOrganizacion.SelectedIndex;
            if (indiceDeOrganizacion > -1)
            {
                encargado.Organizacion = Organizaciones[indiceDeOrganizacion];
                if (ValidarEncargado(encargado))
                {
                    bool resultadoDeCreacionDeEncargado = false;
                    AdministradorDeEncargados administradorDeEncargados = new AdministradorDeEncargados();
                    try
                    {
                        resultadoDeCreacionDeEncargado = administradorDeEncargados.CrearEncargado(encargado);
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
                    {
                        MessageBox.Show("No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos)
                    {
                        MessageBox.Show("No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    if (resultadoDeCreacionDeEncargado)
                    {
                        MessageBox.Show("Encargado registrado correctamente.");

                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una organizacion.");
            }
        }

        public bool ValidarEncargado(Encargado encargado)
        {
            bool resultadoDeValidacion = false;
            if (ValidarNombre(encargado.Nombre))
            {
                if (ValidarCorreoElectronico(encargado.CorreoElectronico))
                {
                    if (TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text)
                    {
                        if (ValidarTelefono(encargado.Telefono))
                        {
                            resultadoDeValidacion = true;
                        }
                        else
                        {
                            TextBoxTelefono.BorderBrush = Brushes.Red;
                        }
                    }
                    else
                    {
                        TextBoxConfirmarCorreoElectronico.BorderBrush = Brushes.Red;
                    }
                }
                else
                {
                    TextBoxCorreoElectronico.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                TextBoxNombre.BorderBrush = Brushes.Red;
            }

            return resultadoDeValidacion;
        }

    }
}
