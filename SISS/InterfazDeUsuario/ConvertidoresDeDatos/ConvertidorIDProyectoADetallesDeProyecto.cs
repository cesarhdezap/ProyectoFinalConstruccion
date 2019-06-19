using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using LogicaDeNegocios;
using LogicaDeNegocios.Excepciones;
using LogicaDeNegocios.ObjetoAccesoDeDatos;

namespace InterfazDeUsuario
{
    class ConvertidorIDProyectoADetallesDeProyecto : IValueConverter
    {
        public object Convert(object IDProyecto, Type cadena, object parametro, CultureInfo culture)
        {
            ProyectoDAO proyectoDAO = new ProyectoDAO();
            EncargadoDAO encargadoDAO = new EncargadoDAO();
            OrganizacionDAO organizacionDAO = new OrganizacionDAO();
            Proyecto proyecto = new Proyecto();
            Encargado encargado = new Encargado();
            Organizacion organizacion = new Organizacion();

            string cadenaResultado = string.Empty;
            try
            {
                proyecto = proyectoDAO.CargarProyectoPorID((int)IDProyecto);
                encargado = encargadoDAO.CargarIDPorIDProyecto(proyecto.IDProyecto);
                organizacion = organizacionDAO.CargarIDPorIDEncargado(encargado.IDEncargado);
                encargado = encargadoDAO.CargarEncargadoPorID(encargado.IDEncargado);
                organizacion = organizacionDAO.CargarOrganizacionPorID(organizacion.IDOrganizacion);

                cadenaResultado = "-Dependencia: " + organizacion.Nombre + System.Environment.NewLine +
                                     "- Direccion: " + organizacion.Direccion + System.Environment.NewLine +
                                     "- Encargado: " + encargado.Nombre + System.Environment.NewLine +
                                     "- Correo Electronico: " + encargado.CorreoElectronico + System.Environment.NewLine +
                                     "- Cupo: " + proyecto.ObtenerDisponibilidad() + System.Environment.NewLine +
                                     "- Descripcion general: " + proyecto.DescripcionGeneral;
            }
            catch (AccesoADatosException ex)
            {
                MensajeDeErrorParaMessageBox mensaje;
                mensaje = ManejadorDeExcepciones.ManejarExcepcionDeAccesoADatos(ex);
                cadenaResultado = mensaje.Mensaje;
            }

            return cadenaResultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
