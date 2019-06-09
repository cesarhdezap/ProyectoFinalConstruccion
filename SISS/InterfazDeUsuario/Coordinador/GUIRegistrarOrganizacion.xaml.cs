using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetosAdministrador;
using static LogicaDeNegocios.Servicios.ServiciosDeValidacion;

namespace InterfazDeUsuario.GUIsDeCoordinador
{
    public partial class GUIRegistrarOrganizacion : Window
    {
        public GUIRegistrarOrganizacion(DocenteAcademico coordinador)
        {
            InitializeComponent();
            LabelNombreDeUsuario.Content = coordinador.Nombre;
            TextBoxNombre.Text = string.Empty;
        }

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            Organizacion organizacion = new Organizacion()
            {
                Nombre = TextBoxNombre.Text,
                Direccion = TextBoxDireccion.Text,
                Telefono = TextBoxTelefono.Text,
                CorreoElectronico = TextBoxCorreoElectronico.Text
            };

            if (ValidarOrganizacion(organizacion))
            {
                bool resultadoDeCreacionDeOrganizacion = false;
                AdministradorDeOrganizaciones administradorDeOrganizaciones = new AdministradorDeOrganizaciones();

                try
                {
                    resultadoDeCreacionDeOrganizacion = administradorDeOrganizaciones.CrearOrganizacion(organizacion);
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
                    resultadoDeCreacionDeOrganizacion = false;
                }

                if (resultadoDeCreacionDeOrganizacion)
                {
                    MessageBox.Show("Encargado registrado correctamente.");
                }
            }
        }

        private bool ValidarOrganizacion(Organizacion organizacion)
        {
            throw new NotImplementedException();
        }
    }
}
