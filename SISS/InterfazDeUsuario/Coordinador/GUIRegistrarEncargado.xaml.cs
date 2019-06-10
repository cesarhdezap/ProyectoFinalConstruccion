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
            if (indiceDeOrganizacion >= 0)
            {
                encargado.Organizacion = Organizaciones[indiceDeOrganizacion];
                if (TextBoxCorreoElectronico.Text == TextBoxConfirmarCorreoElectronico.Text)
                {
                    AdministradorDeEncargados administradorDeEncargados = new AdministradorDeEncargados();
                    bool resultadoDeCreacion = false;
                    try
                    {
                        resultadoDeCreacion = encargado.GuardarEncargado();
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ConexionABaseDeDatosFallida)
                    {
                        MessageBox.Show("No se pudo establecer conexion al servidor. Porfavor, verfique su conexion e intentelo de nuevo.", "Conexion fallida", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (AccesoADatosException ex) when (ex.TipoDeError == TipoDeError.ErrorDesconocidoDeAccesoABaseDeDatos)
                    {
                        MessageBox.Show("No se pudo accesar a la base de datos por motivos desconocidos, contacte a su administrador.", "Error desconocido", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (resultadoDeCreacion)
                    {
                        MessageBox.Show("Encargado registrado correctamente.");
                        Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una organizacion.");
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
            if (TextBoxCorreoElectronico.Text == TextBoxCorreoElectronico.Text)
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
